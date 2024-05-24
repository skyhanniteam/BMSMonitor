using DevExpress.Xpf.Core;
using SNet3.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SNet3.Monitor
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public static event EventHandler<PageChangedEventArge> PageChangedEvent;
        public static void FiredPageChangeEvent(string pageName)
        {
            PageChangedEvent?.Invoke(null, new PageChangedEventArge(pageName));
        }

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}
