using MvvmHelpers;
using SNet3.Core.SocketUtils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows.Data;

namespace SNet3.Core.Models
{
    public abstract class BankModel : BaseViewModel
    {
        public BankModel()
        {
            Cells = new ObservableRangeCollection<CellModel>();
            BindingOperations.EnableCollectionSynchronization(Cells, cellsLock);

            Modules = new ObservableRangeCollection<ModuleModel>();
            BindingOperations.EnableCollectionSynchronization(Modules, modulesLock);

            Led = new ObservableRangeCollection<bool?>();
            DryContact = new ObservableRangeCollection<bool?>();
            InputSignal = new ObservableRangeCollection<bool?>();
            for (int i = 0; i < 8; i++)
            {
                Led.Add(null);
                DryContact.Add(null);
                InputSignal.Add(null);
            }
            for (int i = 0; i < 8; i++)
                DryContact.Add(null);
        }

        private readonly object cellsLock = new object();
        private readonly object modulesLock = new object();

        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string version;
        public string Version
        {
            get => version;
            set => SetProperty(ref version, value);
        }

        private Definitions.Device.Type deviceType;
        public Definitions.Device.Type DeviceType
        {
            get => deviceType;
            set => SetProperty(ref deviceType, value);
        }

        private string ip;
        public string Ip
        {
            get => ip;
            set => SetProperty(ref ip, value);
        }

        private int unitNumber;
        public int UnitNumber
        {
            get => unitNumber;
            set => SetProperty(ref unitNumber, value);
        }

        private int measuringInterval;
        public int MeasuringInterval
        {
            get => measuringInterval;
            set => SetProperty(ref measuringInterval, value);
        }

        private int siteId;
        public int SiteId
        {
            get => siteId;
            set => SetProperty(ref siteId, value);
        }

        private int cellNumber;
        public int CellNumber
        {
            get => cellNumber;
            set => SetProperty(ref cellNumber, value);
        }

        private string model;
        public string Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        private int? voltType;
        public int? VoltType
        {
            get => voltType;
            set => SetProperty(ref voltType, value);
        }

        private int? capacity;
        public int? Capacity
        {
            get => capacity;
            set => SetProperty(ref capacity, value);
        }

        private string manufacture;
        public string Manufacture
        {
            get => manufacture;
            set => SetProperty(ref manufacture, value);
        }

        private string location;
        public string Location
        {
            get => location;
            set => SetProperty(ref location, value);
        }

        private string memo;
        public string Memo
        {
            get => memo;
            set => SetProperty(ref memo, value);
        }

        private DateTime installDate;
        public DateTime InstallDate
        {
            get => installDate;
            set => SetProperty(ref installDate, value);
        }

        private DateTime manufactureDate;
        public DateTime ManufactureDate
        {
            get => manufactureDate;
            set => SetProperty(ref manufactureDate, value);
        }

        private double? voltage;
        public double? Voltage
        {
            get => voltage;
            set => SetProperty(ref voltage, value);
        }

        private double? current;
        public double? Current
        {
            get => current;
            set => SetProperty(ref current, value);
        }

        private double? rippleCurrent;
        public double? RippleCurrent
        {
            get => rippleCurrent;
            set => SetProperty(ref rippleCurrent, value);
        }

        private double? ambientTemperature;
        public double? AmbientTemperature
        {
            get => ambientTemperature;
            set => SetProperty(ref ambientTemperature, value);
        }

        private double chargeCurrentDetect;
        public double ChargeCurrentDetect
        {
            get => chargeCurrentDetect;
            set => SetProperty(ref chargeCurrentDetect, value);
        }

        private double chargeCurrentRelease;
        public double ChargeCurrentRelease
        {
            get => chargeCurrentRelease;
            set => SetProperty(ref chargeCurrentRelease, value);
        }

        private double? disChargeCurrentDetect;
        [Range(-9000, 9000)]
        public double? DisChargeCurrentDetect
        {
            get => disChargeCurrentDetect;
            set => SetProperty(ref disChargeCurrentDetect, value);
        }

        private double? disChargeCurrentRelease;
        [Range(-9000, 9000)]
        public double? DisChargeCurrentRelease
        {
            get => disChargeCurrentRelease;
            set => SetProperty(ref disChargeCurrentRelease, value);
        }

        private double stringVoltageHighAlarm;
        public double StringVoltageHighAlarm
        {
            get => stringVoltageHighAlarm;
            set => SetProperty(ref stringVoltageHighAlarm, value);
        }

        private double stringVoltageLowAlarm;
        public double StringVoltageLowAlarm
        {
            get => stringVoltageLowAlarm;
            set => SetProperty(ref stringVoltageLowAlarm, value);
        }

        private double stringCurrentHighAlarm;
        public double StringCurrentHighAlarm
        {
            get => stringCurrentHighAlarm;
            set => SetProperty(ref stringCurrentHighAlarm, value);
        }

        private double stringCurrentLowAlarm;
        public double StringCurrentLowAlarm
        {
            get => stringCurrentLowAlarm;
            set => SetProperty(ref stringCurrentLowAlarm, value);
        }

        private double ambientTemperatureHighAlarm;
        public double AmbientTemperatureHighAlarm
        {
            get => ambientTemperatureHighAlarm;
            set => SetProperty(ref ambientTemperatureHighAlarm, value);
        }

        private double ambientTemperatureLowAlarm;
        public double AmbientTemperatureLowAlarm
        {
            get => ambientTemperatureLowAlarm;
            set => SetProperty(ref ambientTemperatureLowAlarm, value);
        }

        private double cellVoltageHighAlarm;
        public double CellVoltageHighAlarm
        {
            get => cellVoltageHighAlarm;
            set => SetProperty(ref cellVoltageHighAlarm, value);
        }

        private double cellVoltageLowAlarm;
        public double CellVoltageLowAlarm
        {
            get => cellVoltageLowAlarm;
            set => SetProperty(ref cellVoltageLowAlarm, value);
        }

        private double cellTemperatureHighAlarm;
        public double CellTemperatureHighAlarm
        {
            get => cellTemperatureHighAlarm;
            set => SetProperty(ref cellTemperatureHighAlarm, value);
        }

        private double cellTemperatureLowAlarm;
        public double CellTemperatureLowAlarm
        {
            get => cellTemperatureLowAlarm;
            set => SetProperty(ref cellTemperatureLowAlarm, value);
        }

        private double resistanceAlarmReference;
        public double ResistanceAlarmReference
        {
            get => resistanceAlarmReference;
            set => SetProperty(ref resistanceAlarmReference, value);
        }

        private double resistanceAlarmWarning;
        public double ResistanceAlarmWarning
        {
            get => resistanceAlarmWarning;
            set => SetProperty(ref resistanceAlarmWarning, value);
        }

        private double resistanceAlarmFail;
        public double ResistanceAlarmFail
        {
            get => resistanceAlarmFail;
            set => SetProperty(ref resistanceAlarmFail, value);
        }

        private bool isMeasuring;
        public bool IsMeasuring
        {
            get => isMeasuring;
            set => SetProperty(ref isMeasuring, value);
        }

        private int? voltageGain;
        [Range(0, 99)]
        public int? VoltageGain
        {
            get => voltageGain;
            set => SetProperty(ref voltageGain, value);
        }


        private int? currentGain;
        [Range(0, 99)]
        public int? CurrentGain
        {
            get => currentGain;
            set => SetProperty(ref currentGain, value);
        }


        private double? currentOffset;
        [Range(-10.0, 10.0)]
        public double? CurrentOffset
        {
            get => currentOffset;
            set => SetProperty(ref currentOffset, value);
        }

        private int? cellVoltagePeriod;
        [Range(2, 3600)]
        public int? CellVoltagePeriod
        {
            get => cellVoltagePeriod;
            set => SetProperty(ref cellVoltagePeriod, value);
        }

        private int? cellResistancePeriod;
        [Range(3, 9999)]
        public int? CellResistancePeriod
        {
            get => cellResistancePeriod;
            set => SetProperty(ref cellResistancePeriod, value);
        }

        private int? unitMax;
        [Range(1, 100)]
        public int? UnitMax
        {
            get => unitMax;
            set => SetProperty(ref unitMax, value);
        }

        private double? voltageDeadband;
        [Range(0.1, 25)]
        public double? VoltageDeadband
        {
            get => voltageDeadband;
            set => SetProperty(ref voltageDeadband, value);
        }

        private double? currentDeadband;
        [Range(0.1, 25)]
        public double? CurrentDeadband
        {
            get => currentDeadband;
            set => SetProperty(ref currentDeadband, value);
        }

        private double? temperatureDeadband;
        [Range(0.1, 25)]
        public double? TemperatureDeadband
        {
            get => temperatureDeadband;
            set => SetProperty(ref temperatureDeadband, value);
        }

        private double? rippleCurrentDeadband;
        [Range(0.1, 25)]
        public double? RippleCurrentDeadband
        {
            get => rippleCurrentDeadband;
            set => SetProperty(ref rippleCurrentDeadband, value);
        }

        private double? cellVoltageDeadband;
        [Range(0.01, 2.5)]
        public double? CellVoltageDeadband
        {
            get => cellVoltageDeadband;
            set => SetProperty(ref cellVoltageDeadband, value);
        }

        private double? cellResistanceDeadband;
        [Range(0.01, 2.5)]
        public double? CellResistanceDeadband
        {
            get => cellResistanceDeadband;
            set => SetProperty(ref cellResistanceDeadband, value);
        }

        private byte[] receivedData;
        public byte[] ReceivedData
        {
            get => receivedData;
            set => SetProperty(ref receivedData, value);
        }

        public ObservableRangeCollection<bool?> Led { get; set; }
        public ObservableRangeCollection<bool?> DryContact { get; set; }
        public ObservableRangeCollection<bool?> InputSignal { get; set; }
        public ObservableRangeCollection<CellModel> Cells { get; set; }
        public ObservableRangeCollection<ModuleModel> Modules { get; set; }

        public abstract void SetData(byte[] data);
        public abstract byte[] MakeRequestByte(Definitions.Device.RequestMethod requestMethod);
        public abstract byte[] MakeRequestByte(Definitions.Device.RequestMethod requestMethod, object parameter);

        public void FiredSendMessageToDeviceEvent(byte[] data)
        {
            try
            {
                ServerSocket.FiredSendMessageToDeviceEvent(Ip, data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private readonly object addCellsLock = new object();
        private readonly object addModulesLock = new object();

        public void AddCell(CellModel newCell)
        {
            lock (addCellsLock)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    for (int i = 0; i < this.Cells.Count; i++)
                    {
                        if (Cells[i].ModuleNumber == newCell.ModuleNumber && Cells[i].Channel == newCell.Channel)
                            return;
                    }
                    this.Cells.Add(newCell);
                }));
            }
        }

        public void AddModule(ModuleModel newModule)
        {
            lock (addModulesLock)
            {
                System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    for (int i = 0; i < this.Modules.Count; i++)
                    {
                        if (Modules[i].Id == newModule.Id)
                            return;
                    }
                    this.Modules.Add(newModule);
                }));
            }
        }
    }
}