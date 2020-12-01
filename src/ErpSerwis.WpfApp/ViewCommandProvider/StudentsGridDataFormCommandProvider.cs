using System.Threading.Tasks;
using System.Windows;
using ErpSerwis.Core.Models;
using ErpSerwis.Core.Repository;
using Telerik.Windows.Controls.Data.DataForm;

namespace ErpSerwis.WpfApp.ViewCommandProvider
{
    public class StudentsGridDataFormCommandProvider : DataFormCommandProvider
    {
        private readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;

        protected override void CommitEdit()
        {
            var completeDatabaseModelCurrentItem = (Students)DataForm.CurrentItem;
            if (null != completeDatabaseModelCurrentItem)
            {
                if (DataForm != null && DataForm.ValidateItem())
                {
                    MessageBoxResult result = MessageBox.Show($"Zapisać dane rekordu Id: {completeDatabaseModelCurrentItem?.Id} do bazy danych?", "Potwierdź operację!", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        _mainWindow.Opacity = 50;
                        _mainWindow.IsEnabled = false;
                        var task = Task.Run(async () =>
                        {
                            completeDatabaseModelCurrentItem = completeDatabaseModelCurrentItem.Id.ToString() == "00000000-0000-0000-0000-000000000000"
                                ? await StudentsRepository.GetInstance().AddAsync(completeDatabaseModelCurrentItem)
                                : await StudentsRepository.GetInstance().SaveAsync(completeDatabaseModelCurrentItem);
                        });
                        task.GetAwaiter().OnCompleted(() =>
                        {
                            base.CommitEdit();
                            MessageBox.Show($"Zapisano dane rekordu Id: {completeDatabaseModelCurrentItem?.Id} do bazy danych.", "Operacja zakończona");
                            _mainWindow.Opacity = 100;
                            _mainWindow.IsEnabled = true;
                        });
                    }
                }
            }
        }

        protected override void Delete()
        {
            var completeDatabaseModelCurrentItem = (Students)DataForm.CurrentItem;
            if (null != completeDatabaseModelCurrentItem)
            {
                if (DataForm != null && DataForm.ValidateItem())
                {
                    MessageBoxResult result = MessageBox.Show($"Usunąć rekord Id: {completeDatabaseModelCurrentItem?.Id} z bazy danych?", "Potwierdź operację!", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        _mainWindow.Opacity = 50;
                        _mainWindow.IsEnabled = false;
                        Task task = Task.Run(async () =>
                        {
                            return await StudentsRepository.GetInstance().DeleteAsync(completeDatabaseModelCurrentItem);
                        });
                        task.GetAwaiter().OnCompleted(() =>
                        {
                            base.Delete();
                            MessageBox.Show($"Usunięto rekord Id: {completeDatabaseModelCurrentItem?.Id} z bazy danych.", "Operacja zakończona");
                            _mainWindow.Opacity = 100;
                            _mainWindow.IsEnabled = true;
                        });
                    }
                }
            }
        }
    }
}
