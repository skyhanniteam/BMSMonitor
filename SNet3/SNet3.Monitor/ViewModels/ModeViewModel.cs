using MvvmHelpers;
using SNet3.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SNet3.Monitor.ViewModels
{
    public class ModeViewModel : BaseViewModel
    {
        public ModeViewModel()
        {
            Title = "Mode";
        }

        public ICommand OnNormalMode => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(SNet3.Core.Definitions.Device.RequestMethod.NormalMode));
            });
        });

        public ICommand OnModbusMode => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(SNet3.Core.Definitions.Device.RequestMethod.ModbusMode));
            });
        });

        public ICommand OnDnpMode => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(SNet3.Core.Definitions.Device.RequestMethod.DnpMode));
            });
        });
    }
}
