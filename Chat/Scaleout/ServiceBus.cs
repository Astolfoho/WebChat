using Microsoft.AspNet.SignalR.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Chat.Scaleout
{
    public class ServiceBus : ScaleoutMessageBus
    {
        private static Bus _bus;

        public ServiceBus(IDependencyResolver resolver, ScaleoutConfiguration configuration, Bus bus) : base(resolver, configuration)
        {
            _bus = bus;
            _bus.OnReceived += _bus_OnReceived;
        }

        private void _bus_OnReceived(object sender, Bus.OnReceivedArgs e)
        {
            this.OnReceived(0, e.Id, e.Message);
        }

        protected override Task Send(IList<Message> messages)
        {
            return Task.Factory.StartNew(() =>
            {
                ScaleoutMessage sm = new ScaleoutMessage(messages);
                _bus.Send(sm);
            });
        }

        protected override void OnReceived(int streamIndex, ulong id, ScaleoutMessage message)
        {
            base.OnReceived(streamIndex, id, message);
        }


    }

    public static class DependencyResolverExtensions
    {
        static Bus _bus;

        static DependencyResolverExtensions()
        {
            _bus = new Bus();
        }
        public static IDependencyResolver UseBus(this IDependencyResolver resolver, ScaleoutConfiguration configuration)
        {
            resolver.Register(typeof(IMessageBus), () => new ServiceBus(resolver, configuration, _bus));
            return resolver;
        }
    }
}
