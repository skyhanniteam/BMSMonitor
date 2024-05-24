using SNet3.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SNet3.Core
{
    public class SocketEventArgs : EventArgs
    {
        public SocketEventArgs(string ip, byte[] data)
        {
            Ip = ip;
            Data = data;
            Time = DateTime.Now;
        }

        public byte[] Data { get; private set; }
        public DateTime Time { get; private set; }
        public string Ip { get; private set; }        
    }

    public class PageChangedEventArge : EventArgs
    {
        public PageChangedEventArge(string pageName)
        {
            PageName = pageName;
        }

        public string PageName { get; set; }
    }

    public class ChangedBankDataEventArgs : EventArgs
    {
        public ChangedBankDataEventArgs(BankModel bank)
        {
            Bank = bank;
        }

        public BankModel Bank { get; set; }
    }
}
