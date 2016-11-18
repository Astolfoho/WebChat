using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using RethinkDb.Driver;
using Scaleout;
using System.Threading;

namespace Chat.Scaleout
{
    public class RethinkBus : ScaleoutMessageBus
    {
        public static RethinkDB R = RethinkDB.R;
        private static RethinkDb.Driver.Net.IConnection _conn;
        private long uid = 0;

        public RethinkBus(IDependencyResolver resolver, ScaleoutConfiguration configuration, RethinkDb.Driver.Net.IConnection conn) : base(resolver, configuration)
        {
            _conn = conn;
            HandleUpdates();
        }

        public void HandleUpdates()
        {
            var t = new Thread(new ThreadStart(async () =>
            {

                try
                {
                    var feed = await R.Db("Chat").Table("messages")
                                                  .Changes().RunChangesAsync<byte[]>(_conn);

                    while (await feed.MoveNextAsync())
                    {
                        var message = feed.Current;
                        var sm = ScaleoutMessage.FromBytes(message.NewValue);
                        var id = Interlocked.Increment(ref uid);
                        this.OnReceived(0, (ulong)id, sm);
                    }
                }
                catch (Exception ex)
                {

                }
            }));
            t.Start();
        }

        protected override Task Send(IList<Message> messages)
        {

            return Task.Factory.StartNew(() =>
            {
                var sm = new ScaleoutMessage(messages);
                R.Db("Chat").Table("messages").Insert(sm.ToBytes()).Run(_conn);
            });
        }



    }

    public static class DependencyRResolverExtensions
    {
        private static RethinkDb.Driver.Net.IConnection _conn;
        public static RethinkDB R = RethinkDB.R;

        static DependencyRResolverExtensions()
        {
            _conn = R.Connection()
             .Hostname("127.0.0.1")
             .Port(RethinkDBConstants.DefaultPort)
             .Timeout(20)
             .Connect();

            try
            {
                R.DbCreate("Chat").Run(_conn);
                R.Db("Chat").TableCreate("messages").Run(_conn);
            }
            catch (Exception)
            { /*Do Nothing*/   }

        }
        public static IDependencyResolver UseRBus(this IDependencyResolver resolver, ScaleoutConfiguration configuration)
        {
            resolver.Register(typeof(IMessageBus), () => new RethinkBus(resolver, configuration, _conn));
            return resolver;
        }
    }
}
