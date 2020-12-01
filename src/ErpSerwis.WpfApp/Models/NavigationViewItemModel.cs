using System.Collections.ObjectModel;

namespace ErpSerwis.WpfApp.Models
{
    public class NavigationViewItemModel
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public ObservableCollection<NavigationViewItemModel> SubItems { get; set; }
    }
}
