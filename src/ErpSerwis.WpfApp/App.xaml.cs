using System.Windows;
using Telerik.Windows.Controls;

namespace ErpSerwis.WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            StyleManager.ApplicationTheme = new MaterialTheme();
        }
    }
}
