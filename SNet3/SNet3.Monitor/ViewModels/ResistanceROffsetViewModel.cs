using MvvmHelpers;
using SNet3.Core.Models;
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
    public class ResistanceROffsetViewModel : BaseViewModel
    {
        public ResistanceROffsetViewModel()
        {
            Title = "Resistance offset";
        }

        private int moduleNumber;
        public int ModuleNumber
        {

            get => moduleNumber;
            set => SetProperty(ref moduleNumber, value);
        }

        private int channel;
        public int Channel
        {

            get => channel;
            set => SetProperty(ref channel, value);
        }

        private double? resistanceOffset;
        public double? ResistanceOffset
        {

            get => resistanceOffset;
            set => SetProperty(ref resistanceOffset, value);
        }

        public ICommand OnUpdate => new ParameterCommandHandler(async (parameter) =>
        {
            await Task.Run(() =>
            {
                var bank = Banks.Instance.Bank;                
                var cells = new List<CellModel>();
                for (int i = 0; i < 4; i++)
                {
                    if (i + 1 == Channel)
                    {
                        cells.Add(new CellModel
                        {
                            ModuleNumber = moduleNumber,
                            Channel = channel,
                            ResistanceOffset = resistanceOffset
                        });
                        continue;
                    }
                                        
                    var selectedCell = bank.Cells.SingleOrDefault(r => r.Channel == i + 1 && r.ModuleNumber == ModuleNumber);
                    if (selectedCell == null)
                        continue;
                    cells.Add(selectedCell);
                }
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(SNet3.Core.Definitions.Device.RequestMethod.SetResistanceOffset, cells));
            });

            (parameter as Window).Close();
        });

        public ICommand OnClose => new ParameterCommandHandler((parameter) =>
        {
            (parameter as Window).Close();
        });
    }
}
