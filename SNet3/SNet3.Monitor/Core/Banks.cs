using MvvmHelpers;
using SNet3.Core;
using SNet3.Core.Device;
using SNet3.Core.SocketUtils;
using SNet3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SNet3.Monitor.Models;
using System.Windows.Data;

namespace SNet3.Monitor.Core
{
    public class Banks
    {
        public event EventHandler ChangedSelectedBankEvent;
        private static Banks instance;
        private static object syncLock = new Object();
        private static object setBankLock = new Object();

        private static readonly object banksLock = new object();
        private static readonly object logsLock = new object();
        public static Banks Instance
        {
            get
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new Banks();
                        instance.banks = new ObservableRangeCollection<BankModel>();
                        instance.Logs = new ObservableRangeCollection<LogModel>();

                        BindingOperations.EnableCollectionSynchronization(instance.banks, banksLock);
                        BindingOperations.EnableCollectionSynchronization(instance.Logs, logsLock);
                    }
                }

                return instance;
            }
        }

        private BankModel bank;
        public BankModel Bank
        {
            get
            {
                if (bank == null)
                    return new Bqms();
                return bank;
            }
            set
            {
                bank = value;
                ChangedSelectedBankEvent?.Invoke(null, new EventArgs());
            }
        }

        private ObservableRangeCollection<BankModel> banks;

        public List<string> SelectIp()
        {
            var ips = new List<string>();
            foreach (var bank in banks)
            {
                ips.Add(bank.Ip);
            }
            return ips;
        }

        public ObservableRangeCollection<LogModel> Logs;

        public void AddLog(LogModel newLog)
        {
            if (!IsStopLog)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Logs.Add(newLog);
                    if (Logs.Count > 20000)
                        Logs.RemoveAt(0);
                }));
            }            
        }

        public void ClearLog()
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() => this.Logs.Clear()));
        }

        public bool IsStopLog { get; set; }

        public void Add(string ip)
        {
            if (!banks.Any(r => r.Ip == ip))
            {
                var newBank = new Bqms
                {
                    Ip = ip
                };
                banks.Add(newBank);

                if (banks.Count == 1)                
                    Bank = newBank;                
            }
        }

        public void SetBank(string ip)
        {
            lock (setBankLock)
            {
                var bank = SelectBank(ip);
                if (bank == null)
                    return;
                Bank = bank;
            }                
        }

        public BankModel SelectBank(string ip)
        {
            return banks.SingleOrDefault(r => r.Ip == ip);
        }
    }
}
