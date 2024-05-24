using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SNet3.Core.Models
{
    public class CellModel : BaseViewModel
    {
        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private int bankId;
        public int BankId
        {
            get => bankId;
            set => SetProperty(ref bankId, value);
        }

        private int channel;
        public int Channel
        {
            get => channel;
            set => SetProperty(ref channel, value);
        }

        private int moduleNumber;
        public int ModuleNumber
        {
            get => moduleNumber;
            set => SetProperty(ref moduleNumber, value);
        }

        private double? voltage;
        public double? Voltage
        {
            get => voltage;
            set => SetProperty(ref voltage, value);
        }

        private double? resistance;
        public double? Resistance
        {
            get => resistance;
            set => SetProperty(ref resistance, value);
        }

        private double? resistanceOffset;
        public double? ResistanceOffset
        {
            get => resistanceOffset;
            set => SetProperty(ref resistanceOffset, value);
        }

        private double? temperature;
        public double? Temperature
        {
            get => temperature;
            set => SetProperty(ref temperature, value);
        }

        private int? gain;
        public int? Gain
        {
            get => gain;
            set => SetProperty(ref gain, value);
        }

        private double? impedanceVoltage;
        public double? ImpedanceVoltage
        {
            get => impedanceVoltage;
            set => SetProperty(ref impedanceVoltage, value);
        }

        private double? impedancecurrent;
        public double? impedanceCurrent
        {
            get => impedancecurrent;
            set => SetProperty(ref impedancecurrent, value);
        }

        private double? phase;
        public double? Phase
        {
            get => phase;
            set => SetProperty(ref phase, value);
        }

        private string note;
        public string Note
        {
            get => note;
            set => SetProperty(ref note, value);
        }

        private DateTime? measureResistanceUpdateTime;
        public DateTime? MeasureResistanceUpdateTime
        {
            get => measureResistanceUpdateTime;
            set => SetProperty(ref measureResistanceUpdateTime, value);
        }

        private DateTime? testMeasureResistanceUpdateTime;
        public DateTime? TestMeasureResistanceUpdateTime
        {
            get => testMeasureResistanceUpdateTime;
            set => SetProperty(ref testMeasureResistanceUpdateTime, value);
        }

        private DateTime? updateTime;
        public DateTime? UpdateTime
        {
            get => updateTime;
            set => SetProperty(ref updateTime, value);
        }
    }
}
