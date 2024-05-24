using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SNet3.Core.Models
{
    public class ModuleModel : BaseViewModel
    {
        public ModuleModel()
        {
            Voltages = new ObservableRangeCollection<double?>();
            BindingOperations.EnableCollectionSynchronization(Voltages, voltagesLock);
            Voltages.Add(null);
            Voltages.Add(null);
            Voltages.Add(null);
            Voltages.Add(null);
        }

        private readonly object voltagesLock = new object();

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

        private int channelCount;
        public int ChannelCount
        {
            get => channelCount;
            set => SetProperty(ref channelCount, value);
        }


        private ObservableRangeCollection<double?> voltages;
        public ObservableRangeCollection<double?> Voltages
        {
            get => voltages;
            set => SetProperty(ref voltages, value);
        }

        private double? voltageOffset;
        [Range(-9.999, 9.999)]
        public double? VoltageOffset
        {
            get => voltageOffset;
            set => SetProperty(ref voltageOffset, value);
        }

        private double? phaseCompensation;
        [Range(-99.999, 99.999)]
        public double? PhaseCompensation
        {
            get => phaseCompensation;
            set => SetProperty(ref phaseCompensation, value);
        }

        private DateTime updateTime;
        [Range(-99.999, 99.999)]
        public DateTime UpdateTime
        {
            get => updateTime;
            set => SetProperty(ref updateTime, value);
        }
    }
}
