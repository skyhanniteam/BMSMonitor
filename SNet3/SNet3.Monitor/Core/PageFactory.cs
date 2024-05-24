using SNet3.Monitor.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SNet3.Monitor.Core
{
    public class PageFactory
    {
        static Dictionary<string, Page> menuDictionary = new Dictionary<string, Page>();

        public static Page GetPage(string name)
        {
            var selectedPage = menuDictionary.SingleOrDefault(r => r.Key == name).Value;

            if (selectedPage == null)
            {
                Page newPage = null;
                switch (name)
                {
                    case "Resistance":
                        newPage = new ResistancePage();
                        break;
                    case "Bank":
                        newPage = new BankPage();
                        break;
                    case "Mode":
                        newPage = new ModePage();
                        break;
                    case "TestMeasure":
                        newPage = new TestMeasurePage();
                        break;
                    case "OutContact":
                        newPage = new OutContactPage();
                        break;
                    case "VoltagePhaseCompensation":
                        newPage = new VoltagePhaseCompensationPage();
                        break;                        
                    default:
                        break;
                }
                menuDictionary.Add(name, newPage);
                selectedPage = newPage;
            }

            return selectedPage;
        }
    }
}
