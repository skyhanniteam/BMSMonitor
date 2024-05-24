using SNet3.Core.SocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNet3.Server.Device
{
    public class DeviceServer
    {
        public static void Start()
        {
            Task.Run(() =>
            {
                var socket = new ServerSocket();
                socket.ReceivedDataEvent += Socket_DeviceReceivedDataEvent;
                socket.StartListening(ServerSocket.ServerType.Device);
            });

            Task.Run(() =>
            {
                var socket = new ServerSocket();
                socket.ReceivedDataEvent += Socket_ClientReceivedDataEvent;
                socket.StartListening(ServerSocket.ServerType.Client);
            });
        }

        private static void Socket_DeviceReceivedDataEvent(object sender, Core.SocketEventArgs e)
        {
            try
            {
                if (e.Data[0] == 0xfd && e.Data[1] == 0x42 && e.Data[2] == 0x51 && e.Data[3] == 0xEE && e.Data[4] == 0x40)
                {
                    Console.WriteLine($"[{e.Ip}] - [{DateTime.Now.ToString("yyMMdd-HH:mm:ss")}] count:{e.Data.Length.ToString("###")} - {BitConverter.ToInt16(e.Data, 7) * 0.1}");                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Socket_ClientReceivedDataEvent(object sender, Core.SocketEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
