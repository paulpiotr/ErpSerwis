using System.Collections.ObjectModel;
using System.ComponentModel;
using ErpSerwis.Core.Models;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;

namespace ErpSerwis.WpfApp.ViewModels
{
    public class StudentListViewModel : ViewModelBase
    {
        private ICollectionView _iCollectionViewstudents;

        public ICollectionView ICollectionViewStudents
        {
            get
            {
                if (null == _iCollectionViewstudents)
                {
                    var students = new ObservableCollection<Students>();
                    _iCollectionViewstudents = new QueryableCollectionView(students);
                }
                return _iCollectionViewstudents;
            }
            set
            {
                if (null != value)
                {
                    _iCollectionViewstudents = value;
                }
            }
        }
    }
}
