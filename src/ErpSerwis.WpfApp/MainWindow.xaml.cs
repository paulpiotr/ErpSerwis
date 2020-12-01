using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ErpSerwis.WpfApp.Models;
using ErpSerwis.WpfApp.Views;
using ErpSerwis.WpfApp.Views.Database;

namespace ErpSerwis.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<NavigationViewItemModel> _navigationViewItemList;

        public MainWindow()
        {
            InitializeComponent();
            Opacity = 50;
            IsEnabled = false;
            DataContext = new ViewModels.NavigationViewViewModel();
            var task = Task.Run(() =>
            {
                try
                {
                    var erpSerwisDbContext = new Core.Data.ErpSerwisDbContext();
                    erpSerwisDbContext.CheckForUpdateAndCreateMssqlMdf();
                    erpSerwisDbContext.CheckForUpdateAndMigrate();
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "Błąd tworzenia bazy danych!");
                }
            });
            task.GetAwaiter().OnCompleted(() =>
            {
                Opacity = 100;
                IsEnabled = true;
            });
        }

        private void OnNavigationViewLoaded(object sender, RoutedEventArgs e)
        {
            _navigationViewItemList = new List<NavigationViewItemModel>();
            _navigationViewItemList.AddRange(navigationView.Items.OfType<NavigationViewItemModel>());
            navigationView.Content = new UserControlMainWindow();
        }

        private void OnNavigationViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var currentlyNavigationViewItemModelItem = e.AddedItems[0] as NavigationViewItemModel;
                NetAppCommon.Helpers.Cache.MemoryCacheProvider.GetInstance().Put("currentlyNavigationViewItemModelItem", currentlyNavigationViewItemModelItem, TimeSpan.FromDays(366));
                switch (currentlyNavigationViewItemModelItem.Name)
                {
                    default:
                        navigationView.Content = new UserControlMainWindow();
                        break;
                    case "DatabaseConfiguration":
                        navigationView.Content = new DatabaseConfigurationUserControl();
                        break;
                    case "CompleteDatabase":
                        navigationView.Content = new UserControlCompleteDatabaseView();
                        break;
                    case "StudentList":
                        navigationView.Content = new StudentListView();
                        break;
                }
            }
        }
    }
}
