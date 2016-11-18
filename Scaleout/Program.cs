using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scaleout
{
    class Program
    {
        public static List<TcpClient> _clients;
        public static object _sinc = new object();
        public static ulong _id;

        static void Main(string[] args)
        {
            _clients = new List<TcpClient>();
            var listener = new Listener();
            listener.OnConnection += Listener_OnConnection;
            listener.Start();
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
            }
        }

        private static void Listener_OnConnection(object sender, Listener.ListenerEventArgs e)
        {
            _clients.Add(e.Client);
            var t = new Thread(new ThreadStart(() =>
            {
                while (e.Client.Connected)
                {
                    try
                    {
                        if (e.Client.Available > 0)
                        {
                            var ns = e.Client.GetStream();
                            var data = new byte[e.Client.Available];
                            ns.Read(data, 0, data.Length);

                            var packet = new Packet();
                            packet.Data = data;
                            _id++;
                            packet.Id = _id;

                            BinaryFormatter ser = new BinaryFormatter();
                            MemoryStream ms = new MemoryStream();
                            ser.Serialize(ms, packet);
                            ms.Seek(0, SeekOrigin.Begin);
                            data = new byte[ms.Length];
                            ms.Read(data, 0, data.Length);

                            foreach (var item in _clients)
                            {
                                if (!item.Connected)
                                {
                                    continue;
                                }

                                var ns2 = item.GetStream();
                                ns2.Write(data, 0, data.Length);

                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                    Thread.Sleep(50);
                }

            }));
            t.Start();
        }
    }

    public class Listener
    {

        private Thread _thread;
        private bool _isRunning;

        public event EventHandler<ListenerEventArgs> OnConnection;

        public Listener()
        {
            _thread = new Thread(new ThreadStart(Run));
        }

        private void Run()
        {
            TcpListener _listener = new System.Net.Sockets.TcpListener(IPAddress.Any, 5500);
            //_listener.ExclusiveAddressUse = false;
            _listener.Start();
            while (this._isRunning)
            {
                var client = _listener.AcceptTcpClient();
                if (OnConnection != null)
                {
                    var args = new ListenerEventArgs();
                    args.Client = client;
                    OnConnection(this, args);
                }
            }
        }

        public void Start()
        {
            this._isRunning = true;
            this._thread.Start();
        }

        public void Stop()
        {
            this._isRunning = false;
            this._thread.Abort();
        }

        public class ListenerEventArgs : EventArgs
        {
            public TcpClient Client { get; set; }
        }

    }


    [Serializable]
    public class Packet
    {
        public ulong Id { get; set; }

        public byte[] Data { get; set; }


    }


}
