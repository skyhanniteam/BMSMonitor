using SNet3.Core;
using SNet3.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SNet3.Monitor
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        Mutex mutex = null;
        protected override void OnStartup(StartupEventArgs e)
        {

            var mutexName = "Snet3Monitor";
            bool isCreatedNew = false;
            try
            {
                mutex = new Mutex(true, mutexName, out isCreatedNew);
                if (!isCreatedNew)                                                    
                {
                    MessageBox.Show("Application already started.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Existing...", "Exception thrown");
                Application.Current.Shutdown();
            }

            base.OnStartup(e);            
            ServerUtils.StartServer();

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Start();


            var longTimer = new System.Windows.Threading.DispatcherTimer();
            longTimer.Tick += LongTimer_Tick;
            longTimer.Interval = new TimeSpan(0, 0, 10);
            longTimer.Start();
        }

        private async void LongTimer_Tick(object sender, EventArgs e)
        {
            var bank = Banks.Instance.Bank;
            if (bank == null)
                return;
            await Task.Run(() =>
            {
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.Version));
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.ChannelCount));
            }); 
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            var bank = Banks.Instance.Bank;            
            if (bank == null)
                return;
            await Task.Run(() =>
            {
                var sleep = 100;
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.ResistanceOffset));
                Thread.Sleep(sleep);
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.StringGain));
                Thread.Sleep(sleep);
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.CellVoltagePeriod));
                Thread.Sleep(sleep);
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.CellResistancePeriod));
                Thread.Sleep(sleep);
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.Discharge));
                Thread.Sleep(sleep);
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.UnitMax));
                Thread.Sleep(sleep);
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.Deadband));
                Thread.Sleep(sleep);
                bank.FiredSendMessageToDeviceEvent(bank.MakeRequestByte(Definitions.Device.RequestMethod.VoltagePhaseCompensation));                
            });            
        }
    }
}
