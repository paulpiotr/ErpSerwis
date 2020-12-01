using System.Threading.Tasks;
using System.Windows;
using ErpSerwis.Core.Models.Database;
using ErpSerwis.Core.Repository;
using Telerik.Windows.Controls.Data.DataForm;

namespace ErpSerwis.WpfApp.ViewCommandProvider.Database
{
    public class CompleteDatabaseCommandProvider : DataFormCommandProvider
    {

        private readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;

        protected override void CommitEdit()
        {
            var completeDatabaseModelCurrentItem = (CompleteDatabaseModel)DataForm.CurrentItem;
            if (null != completeDatabaseModelCurrentItem && completeDatabaseModelCurrentItem.ConfirmTheOperation)
            {
                MessageBoxResult result = MessageBox.Show($"Dodać {completeDatabaseModelCurrentItem?.NumberOfRecords} rekordów do bazy danych? Uwaga istniejące dane zostaną usunięte z bazy danych!", "Potwierdź operację!", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    if (DataForm != null && DataForm.ValidateItem())
                    {
                        _mainWindow.Opacity = 50;
                        _mainWindow.IsEnabled = false;
                        Task task = Task.Run(() =>
                        {
                            return StudentsRepository.GetInstance().CreateExampleStudents((int)completeDatabaseModelCurrentItem?.NumberOfRecords);
                        });
                        task.GetAwaiter().OnCompleted(() =>
                        {
                            MessageBox.Show($"Dodałem {completeDatabaseModelCurrentItem?.NumberOfRecords} rekordów do bazy danych.", "Operacja zakończona");
                            _mainWindow.Opacity = 100;
                            _mainWindow.IsEnabled = true;
                        });
                    }
                }
            }
            else
            {
                MessageBox.Show($"Proszę zaznaczyć potwierdź operację!", "Potwierdź operację!", MessageBoxButton.OK);
            }
        }
    }
}
