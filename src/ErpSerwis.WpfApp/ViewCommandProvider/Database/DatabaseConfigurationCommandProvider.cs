using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ErpSerwis.Core.Models;
using ErpSerwis.Core.Repository;
using Telerik.Windows.Controls.Data.DataForm;

namespace ErpSerwis.WpfApp.ViewCommandProvider.Database
{
    public class DatabaseConfigurationCommandProvider : DataFormCommandProvider
    {

        private readonly MainWindow _mainWindow = (MainWindow)Application.Current.MainWindow;

        protected override void CommitEdit()
        {
            var appSettingsCurrentItem = (AppSettings)DataForm.CurrentItem;
            if (null != appSettingsCurrentItem)
            {
                if (appSettingsCurrentItem.CheckForUpdateAndMigrate)
                {
                    _mainWindow.Opacity = 50;
                    _mainWindow.IsEnabled = false;
                    var message = new StringBuilder();
                    var task = Task.Run(() =>
                    {
                        var erpSerwisDbContext = new Core.Data.ErpSerwisDbContext();
                        var isCheckForUpdateAndCreateMssqlMdf = erpSerwisDbContext.CheckForUpdateAndCreateMssqlMdf(appSettingsCurrentItem);
                        var isCheckForUpdateAndMigrate = erpSerwisDbContext.CheckForUpdateAndMigrate(appSettingsCurrentItem);
                        if (isCheckForUpdateAndCreateMssqlMdf)
                        {
                            message.Append($"Sprawdziłem i utworzyłem bazę danych w formacie Ms SQL MDF w dniu: { appSettingsCurrentItem.LastMigrateDateTime }").Append(Environment.NewLine);
                        }
                        else
                        {
                            message.Append($"Błąd tworzenia bazy danych w formacie Ms SQL MDF!").Append(Environment.NewLine);
                        }
                        if (isCheckForUpdateAndMigrate)
                        {
                            message.Append($"Sprawdziłem i przeprowadziłem migrację w dniu: { appSettingsCurrentItem.LastMigrateDateTime }").Append(Environment.NewLine);
                        }
                        else
                        {
                            message.Append($"Błąd migracji bazy danych!").Append(Environment.NewLine);
                        }
                    });
                    task.GetAwaiter().OnCompleted(() =>
                    {
                        MessageBox.Show(message.ToString(), "Status operacji");
                        _mainWindow.Opacity = 100;
                        _mainWindow.IsEnabled = true;
                    });
                }
                if (DataForm != null && DataForm.ValidateItem())
                {
                    var saveResult = false;
                    _mainWindow.Opacity = 50;
                    _mainWindow.IsEnabled = false;
                    Task task = Task.Run(async () =>
                    {
                        saveResult = await AppSettingsRepository.GetInstance().SaveAsync(appSettingsCurrentItem);
                        return saveResult;
                    });
                    task.GetAwaiter().OnCompleted(() =>
                    {
                        if (saveResult)
                        {
                            MessageBox.Show($"Zapisano ustawienia w lokalizacji { appSettingsCurrentItem.GetFilePath() }", "Operacja zakończona.");
                        }
                        else
                        {
                            MessageBox.Show($"Nie można zapisać ustawień w lokalizacji { appSettingsCurrentItem.GetFilePath() }!", "Błąd!");
                        }
                        _mainWindow.Opacity = 100;
                        _mainWindow.IsEnabled = true;
                    });
                }
            }
        }
    }
}
