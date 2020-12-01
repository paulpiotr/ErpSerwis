using System.IO;
using System.Windows.Controls;

namespace ErpSerwis.WpfApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlMainWindow.xaml
    /// </summary>
    public partial class UserControlMainWindow : UserControl
    {
        public UserControlMainWindow()
        {
            InitializeComponent();
            Viewer.Markdown = File.ReadAllText("README.md");
        }
    }
}
