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
    public class VoltagePhaseCompensationUpdateViewModel : BaseViewModel
    {
        public VoltagePhaseCompensationUpdateViewModel()
        {
            Title = "VoltagePhaseCompensationUpdate";
        }

        private int id;
        public int Id
        {

            get => id;
            set => SetProperty(ref id, value);
        }

        private double? voltageOffset;
        public double? VoltageOffset
        {

            get => voltageOffset;
            set => SetProperty(ref voltageOffset, value);
        }

        private double? phaseCompensation;
        public double? PhaseCompensation
        {

            get => phaseCompensation;
            set => SetProperty(ref phaseCompensation, value);
        }

        public ICommand OnUpdate => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;

                var data = new double?[3];
                data[0] = Id;
                data[1] = VoltageOffset;
                data[2] = phaseCompensation;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(SNet3.Core.Definitions.Device.RequestMethod.VoltagePhaseCompensation, data));              
            });

            (parameter as Window).Close();
        });

        public ICommand OnClose => new ParameterCommandHandler((parameter) =>
        {
            (parameter as Window).Close();
        });
    }
}
