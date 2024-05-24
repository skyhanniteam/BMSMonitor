using SNet3.Core.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNet3.Core.Device
{
    public class Bqms : BankModel
    {
        private byte[] MakeCanBytes(Definitions.Device.Bqms.CanId canId, object parameter)
        {
            var bytes = new byte[16];
           
            return bytes;
        }

        public override byte[] MakeRequestByte(Definitions.Device.RequestMethod requestMethod)
        {
            return MakeRequestByte(requestMethod, null);
        }

        public override byte[] MakeRequestByte(Definitions.Device.RequestMethod requestMethod, object parameter)
        {
            switch (requestMethod)
            {
                case Definitions.Device.RequestMethod.MeasureResistance:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestMeasureResistance, parameter);
                case Definitions.Device.RequestMethod.ResistanceOffset:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestResistanceOffset, parameter);
                case Definitions.Device.RequestMethod.SetResistanceOffset:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestSetResistanceOffset, parameter);
                case Definitions.Device.RequestMethod.Version:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.Version, parameter);
                case Definitions.Device.RequestMethod.StringGain:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestStringGain, parameter);
                case Definitions.Device.RequestMethod.NormalMode:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestNormalMode, parameter);
                case Definitions.Device.RequestMethod.ModbusMode:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestModbusMode, parameter);
                case Definitions.Device.RequestMethod.DnpMode:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestDnpMode, parameter);
                case Definitions.Device.RequestMethod.CellVoltagePeriod:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.CellVoltagePeriod, parameter);
                case Definitions.Device.RequestMethod.CellResistancePeriod:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.CellResistancePeriod, parameter);
                case Definitions.Device.RequestMethod.Discharge:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.Discharge, parameter);
                case Definitions.Device.RequestMethod.UnitMax:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.UnitMax, parameter);
                case Definitions.Device.RequestMethod.Deadband:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.Deadband, parameter);
                case Definitions.Device.RequestMethod.TestMeasure:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestTestMeasure, parameter);
                case Definitions.Device.RequestMethod.OutContract:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestOutContract, parameter);
                case Definitions.Device.RequestMethod.VoltagePhaseCompensation:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestVoltagePhaseCompensation, parameter);
                case Definitions.Device.RequestMethod.ChannelCount:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.RequestChannelCount, parameter);
                case Definitions.Device.RequestMethod.Reset:
                    return MakeCanBytes(Definitions.Device.Bqms.CanId.Reset, parameter);
                default:
                    break;
            }
            return null;
        }

        public override void SetData(byte[] data)
        {
            try
            {
                if (data == null)
                    return;
                if (data[0] != Definitions.Device.Bqms.Head)
                    return;
                if (data[1] != Definitions.Device.Bqms.Id[0] || data[2] != Definitions.Device.Bqms.Id[1])
                    return;

                var canId = (Definitions.Device.Bqms.CanId)BitConverter.ToInt32(data, 3);                
                switch (canId)
                {
                    case Definitions.Device.Bqms.CanId.StringVCT:
                        Voltage = BitConverter.ToInt16(data, 7) * 0.1;
                        Current = (double)ExBitConverter.ToInt24(data, 9) * 0.1;
                        if (data[12] == 0)
                        {
                            var temperature = BitConverter.ToInt16(data, 13);
                            if (temperature == 0x7fff)
                                AmbientTemperature = null;
                            else
                                AmbientTemperature = temperature * 0.1;
                        }
                        else
                            RippleCurrent = BitConverter.ToInt16(data, 13) * 0.1;
                        return;
                    case Definitions.Device.Bqms.CanId.StringGain:
                        VoltageGain = data[8];
                        CurrentGain = data[9];
                        CurrentOffset = (sbyte)data[10] * 0.1;
                        break;
                    case Definitions.Device.Bqms.CanId.StartMeasureResistance:                        
                        IsMeasuring = true;
                        return;
                    case Definitions.Device.Bqms.CanId.EndMeasureResistance:
                        IsMeasuring = false;
                        return;
                    case Definitions.Device.Bqms.CanId.Version:
                        Version = $"{(int)data[7]}.{(int)data[8]}";
                        return;
                    case Definitions.Device.Bqms.CanId.CellVoltagePeriod:
                        CellVoltagePeriod = BitConverter.ToInt16(data, 8);
                        return;
                    case Definitions.Device.Bqms.CanId.CellResistancePeriod:
                        CellResistancePeriod = BitConverter.ToInt16(data, 8);
                        return;
                    case Definitions.Device.Bqms.CanId.Discharge:
                        DisChargeCurrentDetect = (double)ExBitConverter.ToInt24(data, 8) * 0.1;
                        DisChargeCurrentRelease = (double)ExBitConverter.ToInt24(data, 11) * 0.1; 
                        return;
                    case Definitions.Device.Bqms.CanId.UnitMax:
                        UnitMax = (int)data[8];
                        return;
                    case Definitions.Device.Bqms.CanId.Deadband:
                        VoltageDeadband = (double)data[8] * 0.1;
                        CurrentDeadband = (double)data[9] * 0.1;
                        TemperatureDeadband = (double)data[10] * 0.1;
                        RippleCurrentDeadband = (double)data[11] * 0.1;
                        CellVoltageDeadband = (double)data[12] * 0.01;
                        CellResistanceDeadband = (double)data[13] * 0.01;
                        break;
                    case Definitions.Device.Bqms.CanId.OutContract:
                        var dryContactBitArray = new BitArray(new byte[2] { data[7], data[8] });
                        for (int i = 0; i < DryContact.Count(); i++)
                        {
                            if (dryContactBitArray.Count <= i)
                                DryContact[i] = false;
                            else
                                DryContact[i] = dryContactBitArray[i];
                        }

                        var inputSignalBitArray = new BitArray(new byte[1] {  data[9] });
                        for (int i = 0; i < InputSignal.Count(); i++)
                        {
                            if (inputSignalBitArray.Count <= i)
                                InputSignal[i] = false;
                            else
                                InputSignal[i] = inputSignalBitArray[i];
                        }
                        var ledBitArray = new BitArray(new byte[1] { data[10] });
                        for (int i = 0; i < Led.Count(); i++)
                        {
                            if (ledBitArray.Count <= i)
                                Led[i] = false;
                            else
                                Led[i] = ledBitArray[i];
                        }
                        break;                        
                    default:
                        break;
                }

                canId = (Definitions.Device.Bqms.CanId)(BitConverter.ToInt32(data, 3) & 0xFFFFFF00);
                
                int moduleNumber = data[3];
                if (moduleNumber < 1)
                    return;
                CellModel cell = null;
                
                switch (canId)
                {
                    case Definitions.Device.Bqms.CanId.ChannelCount:
                        var channelModule = Modules.SingleOrDefault(r => r.Id == moduleNumber);
                        if (channelModule != null)
                            channelModule.ChannelCount = data[7];
                        break;
                    case Definitions.Device.Bqms.CanId.VoltagePhaseCompensation:
                        var module = Modules.SingleOrDefault(r => r.Id == moduleNumber);
                        var voltageOffset = BitConverter.ToInt16(data, 8) * 0.001;
                        var phaseCompensation = ExBitConverter.ToInt24(data, 10) * 0.001;
                        if (module == null)
                        {
                            module = new ModuleModel
                            {
                                Id = moduleNumber,
                                BankId = Id,
                                VoltageOffset = voltageOffset,
                                PhaseCompensation = phaseCompensation,
                                UpdateTime = DateTime.Now
                            };
                            AddModule(module);
                        }
                        else
                        {
                            module.VoltageOffset = voltageOffset;
                            module.PhaseCompensation = phaseCompensation;
                            module.UpdateTime = DateTime.Now;
                        }
                        return;
                    case Definitions.Device.Bqms.CanId.CellVoltage:
                        for (int i = 0; i < 4; i++)
                        {
                            var voltage = BitConverter.ToInt16(data, i * 2 + 7) * 0.001;
                            cell = Cells.SingleOrDefault(r => r.ModuleNumber == moduleNumber && r.Channel == i + 1);
                            if (cell == null)
                            {
                                cell = new CellModel
                                {
                                    ModuleNumber = moduleNumber,
                                    Channel = i + 1,
                                    Voltage = voltage,
                                    UpdateTime = DateTime.Now
                                };
                                AddCell(cell);
                            }
                            else
                            {
                                cell.Voltage = voltage;
                                cell.UpdateTime = DateTime.Now;
                            }

                            var tempModule = Modules.SingleOrDefault(r => r.Id == moduleNumber);
                            if (tempModule != null)
                                tempModule.Voltages[i] = voltage;
                        }
                        return;
                    case Definitions.Device.Bqms.CanId.CellTemperature:
                        for (int i = 0; i < 4; i++)
                        {
                            double? temperature = BitConverter.ToInt16(data, i * 2 + 7);
                            if (temperature == 0x7FFF)
                                temperature = null;
                            else
                                temperature *= 0.1;
                            cell = Cells.SingleOrDefault(r => r.ModuleNumber == moduleNumber && r.Channel == i + 1);
                            if (cell == null)
                                AddCell(new CellModel
                                {
                                    ModuleNumber = moduleNumber,
                                    Channel = i + 1,
                                    Temperature = temperature,
                                    UpdateTime = DateTime.Now
                                });
                            else
                            {
                                cell.Temperature = temperature;
                                cell.UpdateTime = DateTime.Now;
                            }

                        }
                        return ;
                    case Definitions.Device.Bqms.CanId.CellResistance:
                        var channel = data[7];
                        double? resistance = ExBitConverter.ToInt24(data, 8);

                        if (resistance == 0x7FFFFF)
                            resistance = null;
                        else
                            resistance *= 0.001;
                        double resistanceOffset = BitConverter.ToInt16(data, 11) * 0.01;
                        cell = Cells.SingleOrDefault(r => r.ModuleNumber == moduleNumber && r.Channel == channel);
                        if (cell == null)
                            AddCell(new CellModel
                            {
                                ModuleNumber = moduleNumber,
                                Channel = channel,
                                Resistance = resistance,
                                ResistanceOffset = resistanceOffset,
                                MeasureResistanceUpdateTime = DateTime.Now,
                                UpdateTime = DateTime.Now
                            });
                        else
                        {
                            cell.Resistance = resistance;
                            cell.ResistanceOffset = resistanceOffset;
                            cell.MeasureResistanceUpdateTime = DateTime.Now;
                            cell.UpdateTime = DateTime.Now;
                        }
                        return ;
                    case Definitions.Device.Bqms.CanId.ResistanceOffset:
                        for (int i = 0; i < 4; i++)
                        {
                            var offset = BitConverter.ToInt16(data, i * 2 + 7) * 0.01;
                            cell = Cells.SingleOrDefault(r => r.ModuleNumber == moduleNumber && r.Channel == i + 1);
                            if (cell == null)
                            {
                                cell = new CellModel
                                {
                                    ModuleNumber = moduleNumber,
                                    Channel = i + 1,
                                    ResistanceOffset = offset,
                                    UpdateTime = DateTime.Now
                                };
                                AddCell(cell);
                            }
                            else
                            {
                                cell.ResistanceOffset = offset;
                                cell.UpdateTime = DateTime.Now;
                            }
                        }
                        return;
                    case Definitions.Device.Bqms.CanId.TestMeasure:                        
                        var receiveChannel = (byte)((data[8] >> 4) & 0x0F);
                        cell = Cells.SingleOrDefault(r => r.ModuleNumber == moduleNumber && r.Channel == receiveChannel);
                        var receiveGain = (byte)(data[8] & 0x0F);
                        if (cell == null)
                        {
                            cell = new CellModel
                            {
                                ModuleNumber = moduleNumber,
                                Channel = receiveChannel,
                                Gain = receiveGain,
                                UpdateTime = DateTime.Now,
                                TestMeasureResistanceUpdateTime = DateTime.Now
                            };

                            if (data[7] == 0x01)
                            {
                                cell.ImpedanceVoltage = (double)ExBitConverter.ToInt24(data, 9) * 0.001;
                                cell.impedanceCurrent= (double)ExBitConverter.ToInt24(data, 12) * 0.001;
                            }
                            else
                            {
                                cell.Phase = (double)ExBitConverter.ToInt24(data, 9) * 0.001;
                                cell.Resistance= (double)ExBitConverter.ToInt24(data, 12) * 0.001;
                            }
                            AddCell(cell);
                        }
                        else
                        {
                            cell.Gain = receiveGain;
                            if (data[7] == 0x01)
                            {
                                cell.ImpedanceVoltage = (double)ExBitConverter.ToInt24(data, 9) * 0.001;
                                cell.impedanceCurrent = (double)ExBitConverter.ToInt24(data, 12) * 0.001;
                            }
                            else
                            {
                                cell.Phase = (double)ExBitConverter.ToInt24(data, 9) * 0.001;
                                cell.Resistance = (double)ExBitConverter.ToInt24(data, 12) * 0.001;
                            }
                            cell.UpdateTime = DateTime.Now;
                            cell.TestMeasureResistanceUpdateTime = DateTime.Now;
                        }
                        return;
                    default:
                        break;
                }

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
