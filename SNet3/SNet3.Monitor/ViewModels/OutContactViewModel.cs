using MvvmHelpers;
using SNet3.Core.Models;
using SNet3.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SNet3.Monitor.ViewModels
{
    public class OutContactViewModel : BaseViewModel
    {
        public OutContactViewModel()
        {
            Title = "Module";
            var banks = Banks.Instance;
            banks.ChangedSelectedBankEvent += Banks_ChangedSelectedBankEvent;
            bankModel = banks.Bank;
        }

        private void Banks_ChangedSelectedBankEvent(object sender, EventArgs e)
        {
            BankModel = Banks.Instance.Bank;
        }

        private BankModel bankModel;
        public BankModel BankModel
        {
            get => bankModel;
            set => SetProperty(ref bankModel, value);
        }

        public ICommand OnUpdate => new CommandHandler(async () =>
        {
            var data = new bool[32];

            for (int i = 0; i < BankModel.DryContact.Count; i++)
                data[i] = BankModel.DryContact[i] ?? false;
            for (int i = 0; i < BankModel.InputSignal.Count; i++)
                data[i + 16] = BankModel.InputSignal[i] ?? false;
            for (int i = 0; i < BankModel.Led.Count; i++)
                data[i + 24] = BankModel.Led[i] ?? false;

            await Task.Run(() =>
            {                
                var bank = Banks.Instance.Bank;                
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(SNet3.Core.Definitions.Device.RequestMethod.OutContract, data));
            });
        });
    }
}
