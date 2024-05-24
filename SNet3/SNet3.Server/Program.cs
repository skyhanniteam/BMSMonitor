using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNet3.Server
{
    class Program
    {

        static void Main(string[] args)
        {
            Device.DeviceServer.Start();
            Console.ReadLine();
        }

    }
}
