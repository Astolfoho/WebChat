using Microsoft.AspNet.SignalR.Messaging;
using Scaleout;
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

namespace Chat.Scaleout
{
    public class Bus
    {
        private TcpClient _tcpClient;

        public Bus()
        {
            var ip = new IPEndPoint(IPAddress.Loopback, 5500);
            _tcpClient = new TcpClient();
            _tcpClient.Connect(ip);
            Thread t = new Thread(new ThreadStart(() =>
            {
                while (_tcpClient.Connected)
                {
                    if (_tcpClient.Available > 0)
                    {
                        try
                        {
                            var ns = _tcpClient.GetStream();
                            var data = new byte[_tcpClient.Available];
                            ns.Read(data, 0, data.Length);
                            this.OnReceived_P(data);
                        }
                        catch (Exception)
                        {

                        }
                    }
                    Thread.Sleep(50);
                }
            }));
            t.Start();
        }

        public event EventHandler<OnReceivedArgs> OnReceived;

        private void OnReceived_P(byte[] data)
        {
            BinaryFormatter ser = new BinaryFormatter();
            Packet packet = null;
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(data, 0, data.Length);
                ms.Seek(0, SeekOrigin.Begin);
                packet = ser.Deserialize(ms) as Packet;
            }


            var message = ScaleoutMessage.FromBytes(packet.Data);
            var args = new OnReceivedArgs();
            args.Id = packet.Id;
            args.Message = message;
            OnReceived(this, args);
        }



        public void Send(ScaleoutMessage message)
        {
            Task.Factory.StartNew(() =>
            {
                var data = message.ToBytes();
                _tcpClient.GetStream().Write(data, 0, data.Length);
            });
        }

        public class OnReceivedArgs : EventArgs
        {
            public ulong Id { get; internal set; }
            public ScaleoutMessage Message { get; set; }
        }
    }

}
