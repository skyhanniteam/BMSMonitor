using MvvmHelpers;
using SNet3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNet3.Monitor.Models
{
    public class LogModel : BaseViewModel
    {
        public enum Mode { Reqest, Response }

        public LogModel(int id, string message, DateTime time)
        {
            Id = id;
            Message = message;
            Time = time;
        }

        private int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        private Definitions.Device.Bqms.CanId canId;
        public Definitions.Device.Bqms.CanId CanId
        {
            get => canId;
            set => SetProperty(ref canId, value);
        }

        private byte[] data;
        public byte[] Data
        {
            get => data;
            set => SetProperty(ref data, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }

        private DateTime time;
        public DateTime Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }
    }
}
