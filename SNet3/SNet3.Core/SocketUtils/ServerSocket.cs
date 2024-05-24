using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SNet3.Core.SocketUtils
{
    // State object for reading client data asynchronously  
    public class StateObject
    {
        public StateObject()
        {
            ServerSocket.ClientSendMessageToDeviceEvent += CoreEvent_DeviceSendMessageEvent;
        }

        private void CoreEvent_DeviceSendMessageEvent(object sender, SocketEventArgs e)
        {
            if (workSocket.Connected)
            {
                if (((IPEndPoint)workSocket.RemoteEndPoint).Address.ToString() == e.Ip)
                    workSocket.Send(e.Data);
            }            
        }

        // Client  socket.  
        public Socket workSocket = null;
        // Size of receive buffer.  
        public const int BufferSize = 1024;
        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];
        // Received data string.  
        public List<byte> data = new List<byte>();
    }

    public class ServerSocket
    {
        public static event EventHandler<SocketEventArgs> ClientSendMessageToDeviceEvent;
        
        public static void FiredSendMessageToDeviceEvent(string ip, byte[] data)
        {
            ClientSendMessageToDeviceEvent?.Invoke(null, new SocketEventArgs(ip, data));
        }

        public event EventHandler<SocketEventArgs> ConnectedClientEvent;        

        public enum ServerType { Device, Client }
        public event EventHandler<SocketEventArgs> ReceivedDataEvent;
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);        

        public ServerSocket()
        {
        }

        public void StartListening(ServerType serverType)
        {
            // Data buffer for incoming data.  
            var bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".              

            /*
            var ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            var ipAddress = ipHostInfo.AddressList.First(r => r.AddressFamily == AddressFamily.InterNetwork);
            //var ipAddress = IPAddress.Parse("192.168.0.120");
            */

            var ipAddress = IPAddress.Any;
            var localEndPoint = new IPEndPoint(ipAddress, serverType == ServerType.Device ? Definitions.Device.Port : Definitions.Client.Port);

            // Create a TCP/IP socket.  
            var listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and listen for incoming connections.  
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();
                    // Start an asynchronous socket to listen for connections.  
                    Console.WriteLine("Waiting for a connection...");
                    listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
                    // Wait until a connection is made before continuing.  
                    allDone.WaitOne();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            var listener = (Socket)ar.AsyncState;
            var handler = listener.EndAccept(ar);
            var ip = ((IPEndPoint)handler.RemoteEndPoint).Address.ToString();
            Console.WriteLine($"connected to : {ip}");
            ConnectedClientEvent?.Invoke(this, new SocketEventArgs(ip, null));
            // Create the state object.  
            var state = new StateObject
            {
                workSocket = handler
            };
            handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }

        public void ReadCallback(IAsyncResult ar)
        {
            var content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            var state = (StateObject)ar.AsyncState;
            var handler = state.workSocket;

            try
            {
                // Read data from the client socket.   
                var bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There  might be more data, so store the data received so far.  
                    state.data.AddRange(state.buffer.Take(bytesRead));
                    // Check for end-of-file tag. If it is not there, read   
                    // more data.                  
                    if (state.data.Contains(Definitions.Device.Tail))
                    {
                        var listSendData = new List<byte>();
                        foreach (var item in state.data)
                        {
                            listSendData.Add(item);
                            if (listSendData.Contains(Definitions.Device.Tail) && listSendData.Count > 15)
                            {
                                OnRaiseReceivedData(((IPEndPoint)handler.RemoteEndPoint).Address.ToString(), listSendData.ToArray());
                                listSendData.Clear();
                            }
                        }
                        state.data = listSendData;

                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                    }
                    else
                    {
                        // Not all data received. Get more.  
                        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }           
        }

        protected virtual void OnRaiseReceivedData(string ip, byte[] data)
        {
            ReceivedDataEvent?.Invoke(this, new SocketEventArgs(ip, data));
        }
    }
}
