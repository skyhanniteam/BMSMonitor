using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNet3.Core
{
    public partial class Definitions
    {
        public static class Device
        {
            public const byte Tail = 0x;
            public const int Port = 9023;
            public enum Type { Bqms21 }
            public enum RequestMethod
            {
                MeasureResistance,
                ResistanceOffset,
                SetResistanceOffset,
                Version,
                StringGain,
                NormalMode,
                ModbusMode,
                DnpMode,
                CellVoltagePeriod,
                CellResistancePeriod,
                Discharge,
                UnitMax,
                Deadband,
                TestMeasure,
                OutContract,
                VoltagePhaseCompensation,
                ChannelCount,
                Reset
            }

            public static class Bqms
            {
                public const byte Head = 0xFD;                
                public static readonly byte[] Id = new byte[] { 0x42, 0x51 };
                public enum MeasureResistanceType { All = 0, Odd = 1, Even = 2 }

                public enum CanId
                {
                    StringVCT = 0x00,
                    StringGain = 0x00,
                    RequestStringGain = 0x00,
                    CellVoltage = 0x00,
                    CellTemperature = 0x00,
                    CellResistance = 0x00,
                    RequestMeasureResistance = 0x00,
                    Reset = 0x003BB000
                }
            }
        }
    }
}
