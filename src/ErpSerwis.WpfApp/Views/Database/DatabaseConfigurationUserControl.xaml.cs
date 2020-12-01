using System.Windows;
using System.Windows.Controls;
using ErpSerwis.WpfApp.Models;

namespace ErpSerwis.WpfApp.Views.Database
{
    /// <summary>
    /// Logika interakcji dla klasy DatabaseConfigurationUserControl.xaml
    /// </summary>
    public partial class DatabaseConfigurationUserControl : UserControl
    {
        //private readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;

        public DatabaseConfigurationUserControl()
        {
            InitializeComponent();
            var navigationViewItemModel = (NavigationViewItemModel)NetAppCommon.Helpers.Cache.MemoryCacheProvider.GetInstance().Get("currentlyNavigationViewItemModelItem");
            dataFormDatabaseConfiguration.Header = navigationViewItemModel?.Title;
            dataFormDatabaseConfiguration.BeginEdit();
            ///dataFormDatabaseConfiguration.CommandProvider = new ViewCommandProvider.Database.CompleteDatabaseCommandProvider();
        }

        private void DataFormDatabaseConfiguration_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            if (e.PropertyName == "ConnectionStrings" || e.PropertyName == "InitializeTask")
            {
                e.DataField.Visibility = Visibility.Hidden;
            }
            if (e.PropertyName == "ConnectionString")
            {
                e.DataField.MaxWidth = 632.8668 * 1.97;
                //e.DataField = new TextBoxViewField() { Label = e.DataField.Label, DataMemberBinding = e.DataField.DataMemberBinding, MaxWidth = 632.8668 * 2, VerticalAlignment = e.DataField.VerticalAlignment, HorizontalAlignment = e.DataField.HorizontalAlignment, Margin = e.DataField.Margin, Padding = e.DataField.Padding, Style = e.DataField.Style };
            }
        }
    }
}
