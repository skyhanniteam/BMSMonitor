using MvvmHelpers;
using SNet3.Core.Models;
using SNet3.Monitor.Core;
using SNet3.Monitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SNet3.Monitor.ViewModels
{
    public class VoltagePhaseCompensationViewModel : BaseViewModel
    {        
        public VoltagePhaseCompensationViewModel()
        {
            Title = "VoltagePhaseCompensation";
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

        public ICommand OnUpdate => new ParameterCommandHandler((parameter) =>
        {
            var module = parameter as ModuleModel;
            var page = new VoltagePhaseCompensationUpdatePage();
            var dataContext = (page.DataContext as VoltagePhaseCompensationUpdateViewModel);
            dataContext.Id = module.Id;
            dataContext.VoltageOffset = module.VoltageOffset;
            dataContext.PhaseCompensation= module.PhaseCompensation;
            page.ShowDialog();
        });
    }
}
