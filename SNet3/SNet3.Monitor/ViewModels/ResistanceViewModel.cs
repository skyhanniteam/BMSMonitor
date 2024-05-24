using MvvmHelpers;
using SNet3.Core;
using SNet3.Core.Models;
using SNet3.Monitor.Core;
using SNet3.Monitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace SNet3.Monitor.ViewModels
{
    public class ResistanceViewModel : BaseViewModel
    {
        public ResistanceViewModel()
        {
            Title = "Resistance";
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
            set
            {                
                SetProperty(ref bankModel, value);
            }
        }        
 
        public ICommand OnMeasureResistance => new ParameterCommandHandler(async (parameter) =>
        {            
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                var measureType = Definitions.Device.Bqms.MeasureResistanceType.All;
                switch (parameter)
                {
                    case "odd":
                        measureType = Definitions.Device.Bqms.MeasureResistanceType.Odd;
                        break;
                    case "even":
                        measureType = Definitions.Device.Bqms.MeasureResistanceType.Even;
                        break;
                    default:
                        break;
                }
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.MeasureResistance, measureType));
            });            
        });

        public ICommand OnUpdateMeasureOffset => new ParameterCommandHandler((parameter) =>
        {
            var cell = parameter as CellModel;
            var page = new ResistanceROffsetPage();
            var dataContext = (page.DataContext as ResistanceROffsetViewModel);
            dataContext.ModuleNumber = cell.ModuleNumber;
            dataContext.Channel = cell.Channel;
            dataContext.ResistanceOffset = cell.ResistanceOffset;
            page.ShowDialog();
        });
    }
}
