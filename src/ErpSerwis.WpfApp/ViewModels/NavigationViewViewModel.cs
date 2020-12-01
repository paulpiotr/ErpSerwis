using System.Collections.Generic;
using System.Collections.ObjectModel;
using ErpSerwis.WpfApp.Models;

namespace ErpSerwis.WpfApp.ViewModels
{
    public class NavigationViewViewModel
    {
        private NavigationViewItemModel _navigationViewItemModel;

        public NavigationViewItemModel NavigationViewItemModel
        {
            get => _navigationViewItemModel;
            set
            {
                if (_navigationViewItemModel != value)
                {
                    _navigationViewItemModel = value;
                }
            }
        }

        private List<NavigationViewItemModel> _navigationViewItemModelList;

        public List<NavigationViewItemModel> NavigationViewItemModelList
        {
            get => _navigationViewItemModelList;
            set
            {
                if (_navigationViewItemModelList != value)
                {
                    _navigationViewItemModelList = value;
                }
            }
        }

        public NavigationViewViewModel()
        {
            NavigationViewItemModelList = GetList();
        }

        private static List<NavigationViewItemModel> GetList()
        {
            var database = new NavigationViewItemModel() { Icon = "&#xe025;", Title = "Baza danych", Name = "DataBase" };
            database.SubItems = new ObservableCollection<NavigationViewItemModel>
            {
                new NavigationViewItemModel() { Icon = "&#xe13c;", Title = "Konfiguracja bazy danych", Name = "DatabaseConfiguration" },
                new NavigationViewItemModel() { Icon = "&#xe12e;", Title = "Wypełnij bazę danych", Name = "CompleteDatabase" },
            };
            var students = new NavigationViewItemModel() { Icon = "&#xe025;", Title = "Studenci", Name = "Students" };
            students.SubItems = new ObservableCollection<NavigationViewItemModel>
            {
                new NavigationViewItemModel() { Icon = "&#xe613;", Title = "Lista studentów", Name = "StudentList" },
            };
            return new List<NavigationViewItemModel>
            {
                database,
                students,
            };
        }
    }
}
