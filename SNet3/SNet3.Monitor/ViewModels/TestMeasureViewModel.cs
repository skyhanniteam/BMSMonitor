using MvvmHelpers;
using SNet3.Core;
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
    public class TestMeasureViewModel : BaseViewModel
    {
        public TestMeasureViewModel()
        {
            Title = "TestMode";
            var banks = Banks.Instance;
            banks.ChangedSelectedBankEvent += Banks_ChangedSelectedBankEvent;
            bankModel = banks.Bank;
            moduleNumber = 1;
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
        private int moduleNumber;
        public int ModuleNumber
        {
            get => moduleNumber;
            set => SetProperty(ref moduleNumber, value);
        }

        public ICommand OnMeasure => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                var requestParameter = new int?[2];
                switch (parameter)
                {
                    case "all":
                        requestParameter[0] = (int)Definitions.Device.Bqms.MeasureResistanceType.All;
                        requestParameter[1] = null;
                        break;
                    case "odd":
                        requestParameter[0] = (int)Definitions.Device.Bqms.MeasureResistanceType.Odd;
                        requestParameter[1] = null;
                        break;
                    case "even":
                        requestParameter[0] = (int)Definitions.Device.Bqms.MeasureResistanceType.Even                        ;
                        requestParameter[1] = null;
                        break;
                    case "module":
                        requestParameter[0] = (int)Definitions.Device.Bqms.MeasureResistanceType.All;
                        requestParameter[1] = moduleNumber;
                        break;
                    default:
                        break;
                }
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.TestMeasure, requestParameter));
            });
        });
    }
}
