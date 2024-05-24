using MvvmHelpers;
using SNet3.Core;
using SNet3.Core.Models;
using SNet3.Monitor.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SNet3.Monitor.ViewModels
{
    public class BankViewModel : BaseViewModel
    {
        public BankViewModel()
        {
            Title = "Module";
            var banks = Banks.Instance;
            banks.ChangedSelectedBankEvent += Banks_ChangedSelectedBankEvent;
            bankModel = banks.Bank;
            InitSetValue();
        }

        private void InitSetValue()
        {            
            SetVoltageGain = bankModel.VoltageGain;
            SetCurrentGain = bankModel.CurrentGain;
            SetCurrentOffset = bankModel.CurrentOffset;
            SetCellVoltagePeriod = bankModel.CellVoltagePeriod;
            SetCellResistancePeriod = bankModel.CellResistancePeriod;
            SetDisChargeCurrentDetect = bankModel.DisChargeCurrentDetect;
            SetDisChargeCurrentRelease = bankModel.DisChargeCurrentRelease;
            SetUnitMax = bankModel.UnitMax;
            SetVoltageDeadband = bankModel.VoltageDeadband;
            SetCurrentDeadband = bankModel.CurrentDeadband;
            SetTemperatureDeadband = bankModel.TemperatureDeadband;
            SetRippleCurrentDeadband = bankModel.RippleCurrentDeadband;
            SetCellVoltageDeadband = bankModel.CellVoltageDeadband;
            SetCellResistanceDeadband = bankModel.CellResistanceDeadband;
        }

        private void Banks_ChangedSelectedBankEvent(object sender, EventArgs e)
        {
            BankModel = Banks.Instance.Bank;
            InitSetValue();
        }

        private BankModel bankModel;
        public BankModel BankModel
        {
            get => bankModel;
            set => SetProperty(ref bankModel, value);
        }


        private int? setVoltageGain;
        [Range(0, 99)]
        public int? SetVoltageGain
        {
            get => setVoltageGain;
            set => SetProperty(ref setVoltageGain, value);
        }
        
        private int? setCurrentGain;
        [Range(0, 99)]
        public int? SetCurrentGain
        {
            get => setCurrentGain;
            set => SetProperty(ref setCurrentGain, value);
        }
                
        private double? setCurrentOffset;
        [Range(-10.0, 10.0)]
        public double? SetCurrentOffset
        {
            get => setCurrentOffset;
            set => SetProperty(ref setCurrentOffset, value);
        }

        private int? setCellVoltagePeriod;
        [Range(2, 3600)]
        public int? SetCellVoltagePeriod
        {
            get => setCellVoltagePeriod;
            set => SetProperty(ref setCellVoltagePeriod, value);
        }

        private int? setCellResistancePeriod;
        [Range(3, 9999)]
        public int? SetCellResistancePeriod
        {
            get => setCellResistancePeriod;
            set => SetProperty(ref setCellResistancePeriod, value);
        }

        private double? setDisChargeCurrentDetect;
        [Range(-9000, 9000)]
        public double? SetDisChargeCurrentDetect
        {
            get => setDisChargeCurrentDetect;
            set => SetProperty(ref setDisChargeCurrentDetect, value);
        }

        private double? setDisChargeCurrentRelease;
        [Range(-9000, 9000)]
        public double? SetDisChargeCurrentRelease
        {
            get => setDisChargeCurrentRelease;
            set => SetProperty(ref setDisChargeCurrentRelease, value);
        }

        private int? setUnitMax;
        [Range(1, 100)]
        public int? SetUnitMax
        {
            get => setUnitMax;
            set => SetProperty(ref setUnitMax, value);
        }

        private double? setVoltageDeadband;
        [Range(0.1, 25)]
        public double? SetVoltageDeadband
        {
            get => setVoltageDeadband;
            set => SetProperty(ref setVoltageDeadband, value);
        }

        private double? setCurrentDeadband;
        [Range(0.1, 25)]
        public double? SetCurrentDeadband
        {
            get => setCurrentDeadband;
            set => SetProperty(ref setCurrentDeadband, value);
        }

        private double? setTemperatureDeadband;
        [Range(0.1, 25)]
        public double? SetTemperatureDeadband
        {
            get => setTemperatureDeadband;
            set => SetProperty(ref setTemperatureDeadband, value);
        }

        private double? setRippleCurrentDeadband;
        [Range(0.1, 25)]
        public double? SetRippleCurrentDeadband
        {
            get => setRippleCurrentDeadband;
            set => SetProperty(ref setRippleCurrentDeadband, value);
        }

        private double? setCellVoltageDeadband;
        [Range(0.01, 2.5)]
        public double? SetCellVoltageDeadband
        {
            get => setCellVoltageDeadband;
            set => SetProperty(ref setCellVoltageDeadband, value);
        }

        private double? setCellResistanceDeadband;
        [Range(0.01, 2.5)]
        public double? SetCellResistanceDeadband
        {
            get => setCellResistanceDeadband;
            set => SetProperty(ref setCellResistanceDeadband, value);
        }

        public ICommand OnUpdateDeadband => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                var datas = new double?[6];
                datas[0] = bank.VoltageDeadband;
                datas[1] = bank.CurrentDeadband;
                datas[2] = bank.TemperatureDeadband;
                datas[3] = bank.RippleCurrentDeadband;
                datas[4] = bank.CellVoltageDeadband;
                datas[5] = bank.CellResistanceDeadband;
                switch (parameter)
                {
                    case "voltageDeadband":
                        datas[0] = setVoltageDeadband;
                        break;
                    case "currentDeadband":
                        datas[1] = setCurrentDeadband;
                        break;
                    case "temperatureDeadband":
                        datas[2] = setTemperatureDeadband;
                        break;
                    case "rippleCurrentDeadband":
                        datas[3] = setRippleCurrentDeadband;
                        break;
                    case "cellVoltageDeadband":
                        datas[4] = setCellVoltageDeadband;
                        break;
                    case "cellResistanceDeadband":
                        datas[5] = setCellResistanceDeadband;
                        break;
                    default:
                        break;
                }
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.Deadband, datas));
            });
        });

        public ICommand OnUpdateGain => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;

                var data = new double?[3];

                data[0] = bank.VoltageGain;
                data[1] = bank.CurrentGain;
                data[2] = bank.CurrentOffset;

                switch (parameter.ToString())
                {
                    case "VoltageGain":
                        data[0] = setVoltageGain;
                        break;
                    case "CurrentGain":
                        data[1] = setCurrentGain;
                        break;
                    case "CurrentOffset":
                        data[2] = setCurrentOffset;
                        break;
                    default:
                        break;
                }

                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.StringGain, data));                
            });
        });

        public ICommand OnUpdateCellVoltagePeriod => new CommandHandler(async () =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.CellVoltagePeriod, setCellVoltagePeriod ?? 0));
            });
        });

        public ICommand OnReset => new CommandHandler(async () =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.Reset));
            });
        });


        public ICommand OnUpdateCellResistancePeriod => new CommandHandler(async () =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.CellResistancePeriod, setCellResistancePeriod ?? 0));
            });
        });

        public ICommand OnUpdateDischarge => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                var detect = bank.DisChargeCurrentDetect;
                var release = bank.DisChargeCurrentRelease;
                switch (parameter.ToString())
                {
                    case "detect":
                        detect = setDisChargeCurrentDetect;
                        break;
                    case "release":
                        release = setDisChargeCurrentRelease;
                        break;
                    default:
                        break;
                }
                var data = new double?[2];
                data[0] = detect;
                data[1] = release;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.Discharge, data));
            });
        });

        public ICommand OnUpdateUnitMax => new CommandHandler(async () =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.UnitMax, setUnitMax ?? 0));
            });
        });
    }
}
