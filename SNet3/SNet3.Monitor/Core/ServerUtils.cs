using SNet3.Core.SocketUtils;
using SNet3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNet3.Monitor.Core
{
    public class ServerUtils
    {
        public static void StartServer()
        {
            Task.Run(() =>
            {
                var socket = new ServerSocket();
                socket.ReceivedDataEvent += Socket_ReceivedDataEvent;
                socket.ConnectedClientEvent += Socket_ConnectedClientEvent;
                socket.StartListening(ServerSocket.ServerType.Device);                
            });
        }
        
        private static void Socket_ConnectedClientEvent(object sender, SNet3.Core.SocketEventArgs e)
        {
            var bank = Banks.Instance;
            bank.Add(e.Ip);
        }

        private static void Socket_ReceivedDataEvent(object sender, SNet3.Core.SocketEventArgs e)
        {
            try
            {
                var banks = Banks.Instance;
                var bank = banks.SelectBank(e.Ip);
                if (bank != null && banks.Bank.Ip == e.Ip)
                {
                    bank.ReceivedData = e.Data;
                    bank.SetData(e.Data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
