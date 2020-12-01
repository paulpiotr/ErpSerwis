using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using ErpSerwis.Core.Models;
using ErpSerwis.Core.Repository;
using ErpSerwis.WpfApp.ViewModels;
using Telerik.Windows.Data;

namespace ErpSerwis.WpfApp.Views
{
    /// <summary>
    /// Logika interakcji dla klasy StudentListView.xaml
    /// </summary>
    public partial class StudentListView : UserControl
    {
        public StudentListView()
        {
            InitializeComponent();
            var studentListViewModel = new StudentListViewModel();
            var task = Task.Run(async () =>
            {
                studentListViewModel.ICollectionViewStudents = new QueryableCollectionView(new ObservableCollection<Students>((List<Students>)await StudentsRepository.GetInstance().GetStudentsAsync()));
            });
            task.GetAwaiter().OnCompleted(() =>
            {
                studentsGridView.ItemsSource = studentListViewModel.ICollectionViewStudents;
                studentsGridDataForm.ItemsSource = studentListViewModel.ICollectionViewStudents;
                studentsGridView.IsBusy = false;
                studentsGridDataForm.IsEnabled = true;
            });
        }

        private void StudentsForm_EditEnding(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndingEventArgs e)
        {
            studentsGridView.Items.Refresh();
        }

        private void StudentsForm_AddedNewItem(object sender, Telerik.Windows.Controls.Data.DataForm.AddedNewItemEventArgs e)
        {
            studentsGridView.Items.Refresh();
        }

        private void StudentsForm_DeletedItem(object sender, Telerik.Windows.Controls.Data.DataForm.ItemDeletedEventArgs e)
        {
            studentsGridView.Items.Refresh();
        }

        private void StudentsForm_AutoGeneratingField(object sender, Telerik.Windows.Controls.Data.DataForm.AutoGeneratingFieldEventArgs e)
        {
            if (e.PropertyName == "Id")
            {
                e.DataField.IsReadOnly = true;
            }
        }
    }
}
