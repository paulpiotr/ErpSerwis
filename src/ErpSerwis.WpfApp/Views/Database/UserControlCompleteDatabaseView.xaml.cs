using System.Windows.Controls;
using ErpSerwis.WpfApp.Models;

namespace ErpSerwis.WpfApp.Views.Database
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlCompleteDatabaseView.xaml
    /// </summary>
    public partial class UserControlCompleteDatabaseView : UserControl
    {
        public UserControlCompleteDatabaseView()
        {
            InitializeComponent();
            var navigationViewItemModel = (NavigationViewItemModel)NetAppCommon.Helpers.Cache.MemoryCacheProvider.GetInstance().Get("currentlyNavigationViewItemModelItem");
            dataFormCompleteDatabase.Header = navigationViewItemModel?.Title;
            dataFormCompleteDatabase.BeginEdit();
            dataFormCompleteDatabase.CommandProvider = new ViewCommandProvider.Database.CompleteDatabaseCommandProvider();
        }
    }
}
