using MvvmHelpers;
using SNet3.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SNet3.Monitor.ViewModels
{
    public class ChangeIpViewModel : BaseViewModel
    {
        public ChangeIpViewModel()
        {
            Title = "Change Ip";
            SetIp();
        }

        public ObservableRangeCollection<string> Ips { get; set; }

        private string selectedIp;
        public string SelectedIp
        {
            get => selectedIp;
            set => SetProperty(ref selectedIp, value);
        }

        private void SetIp()
        {
            Ips = new ObservableRangeCollection<string>();            
            Ips.AddRange(Banks.Instance.SelectIp());
        }

        public ICommand OnUpdate => new ParameterCommandHandler((parameter) =>
        {
            if (string.IsNullOrEmpty(parameter.ToString()))
            {
                MessageBox.Show("choose..");
                return;
            }

            Banks.Instance.SetBank(selectedIp);            
            (parameter as Window).Close();
        });

        public ICommand OnClose => new ParameterCommandHandler((parameter) =>
        {
            (parameter as Window).Close();
        });
    }
}
