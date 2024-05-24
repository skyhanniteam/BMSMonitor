using MvvmHelpers;
using SNet3.Core.Models;
using SNet3.Monitor.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SNet3.Monitor.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Title = $"Bqms Monitor v{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
            var banks = Banks.Instance;
            banks.ChangedSelectedBankEvent += Banks_ChangedSelectedBankEvent;
            bankModel = banks.Bank;
        }

        private void Banks_ChangedSelectedBankEvent(object sender, EventArgs e)
        {
            BankModel = Banks.Instance.Bank;
        }

        private BankModel bankModel;
        public BankModel BankModel
        {
            get => bankModel;
            set => SetProperty(ref bankModel, value);
        }

        private string selectedPageName;
        public string SelectedPageName
        {
            get => selectedPageName;
            set => SetProperty(ref selectedPageName, value);
        }

        public ICommand OnSetPage => new ParameterCommandHandler(SetPage);

        private void SetPage(object name)
        {
            if (name.ToString() == "ChangeIp")
            {
                new Views.ChangeIpPage().ShowDialog();
                return;
            }
            SelectedPageName = name.ToString();
            (App.Current.MainWindow as MainWindow).frame.Navigate(PageFactory.GetPage(name.ToString()));
            MainWindow.FiredPageChangeEvent(name.ToString());
        }
    }
}
