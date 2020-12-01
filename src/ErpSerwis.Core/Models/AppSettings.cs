using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Reflection;
using MvvmCross.ViewModels;
using NetAppCommon;
using Newtonsoft.Json;

namespace ErpSerwis.Core.Models
{
    #region public partial class AppSettings
    /// <summary>
    /// Klasa modelu ustawień aplikacji ErpSerwis.Core.Models
    /// The settings model class of the ErpSerwis.Core.Models
    /// </summary>
    [NotMapped]
    public partial class AppSettings : MvxViewModel, INotifyPropertyChanged
    {
        #region private static readonly log4net.ILog log4net
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private static readonly log4net.ILog Log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region private static readonly string FileName
        /// <summary>
        /// Nazwa pliku z ustawieniami aplikacji ustawiona w zależności od wersji środowiska
        /// Application settings file name set depending on the version of the environment
        /// </summary>
#if DEBUG
        private static readonly string FileName = "erp.serwis.core.debug.json";
#else
        private static readonly string FileName = "erp.serwis.core.release.json";
#endif
        #endregion

        #region private static readonly string FilePath...
        /// <summary>
        /// Absolutna ścieżka do pliku konfiguracji
        /// The absolute path to the configuration file
        /// </summary>
        private static readonly string FilePath = Path.Combine(Configuration.GetBaseDirectory(), FileName);
        #endregion

        public string GetFilePath()
        {
            return FilePath;
        }

        #region private static readonly string ConnectionStringName
        /// <summary>
        /// Nazwa połączenia bazy danych Mssql dla bieżącej aplikacji
        /// The name of the Mssql database connection for the current application
        /// </summary>
        private static readonly string ConnectionStringName = "ErpSerwisDbContext";
        #endregion

        #region public static AppSettings GetInstance()
        /// <summary>
        /// Pobierz instancję klasy AppSettings
        /// Get an instance of the AppSettings class
        /// </summary>
        /// <returns>
        /// Instanacja klasy AppSettings
        /// Instance of the AppSettings class
        /// </returns>
        public static AppSettings GetInstance()
        {
            return new AppSettings();
        }
        #endregion

        #region public AppSettings()
        /// <summary>
        /// Konstruktor - przypisanie zmiennych z pliku konfiguracyjnego
        /// Constructor - assigning variables from the configuration file
        /// </summary>
        public AppSettings()
        {
            try
            {
                ConnectionString = Configuration.GetValue<string>(FileName, string.Format("{0}:{1}", "ConnectionStrings", ConnectionStringName)) ?? @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=%Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)%\ErpSerwisMSSQLLocalDB\ErpSerwisMSSQLLocalDB.mdf; Database=%AttachDbFilename%; MultipleActiveResultSets=true; Integrated Security=True; Trusted_Connection=Yes";
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            try
            {
                CheckForUpdateEveryDays = Configuration.GetValue<int>(FileName, "CheckForUpdateEveryDays");
                if (CheckForUpdateEveryDays < 0)
                {
                    CheckForUpdateEveryDays = 0;
                }
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            try
            {
                LastMigrateDateTime = Configuration.GetValue<DateTime>(FileName, "LastMigrateDateTime");
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
        }
        #endregion

        private int _checkForUpdateEveryDays;

        #region public int CheckForUpdateEveryDays { get; set; }
        /// <summary>
        /// Czas sprawdzania aktualizacji bazy danych (w dniach)
        /// Checking database migration updates in days
        /// </summary>
        [Editable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [JsonProperty(nameof(CheckForUpdateEveryDays))]
        [Display(Name = "Czas sprawdzania aktualizacji\nbazy danych (w dniach)", Prompt = "Wpisz czas sprawdzania\naktualizacji bazy danych (w dniach)", Description = "Czas sprawdzania aktualizacji\nbazy danych (w dniach)")]
        [Required]
        [Range(0, 2147483647)]
        public int CheckForUpdateEveryDays
        {
            get => _checkForUpdateEveryDays;
            set
            {
                if (value != _checkForUpdateEveryDays)
                {
                    _checkForUpdateEveryDays = value;
                    OnPropertyChanged(nameof(CheckForUpdateEveryDays));
                }
            }
        }
        #endregion

        private bool _checkForUpdateAndMigrate;

        #region public bool CheckForUpdateAndMigrate { get; set; }
        /// <summary>
        /// Sprawdź czy baza danych wymaga aktualizacji,\nprzeprowadź instalację oraz migrację bazy danych
        /// Check if the database needs to be updated and perform the installation and database migration
        /// </summary>
        [JsonIgnore]
        [Display(Name = "Sprawdź czy baza danych wymaga aktualizacji,\nprzeprowadź instalację oraz migrację bazy danych", Prompt = "Zaznacz, jeśli chcesz sprawdzić czy baza danych wymaga aktualizacji i przeprowadź instalację oraz migrację bazy danych", Description = "Sprawdź czy baza danych wymaga aktualizacji,\nprzeprowadź instalację oraz migrację bazy danych")]
        public bool CheckForUpdateAndMigrate
        {
            get => _checkForUpdateAndMigrate;
            set
            {
                if (value != _checkForUpdateAndMigrate)
                {
                    _checkForUpdateAndMigrate = value;
                    OnPropertyChanged(nameof(CheckForUpdateAndMigrate));
                }
            }
        }
        #endregion

        private DateTime _lastMigrateDateTime;

        #region public DateTime LastMigrateDateTime { get; private set; }
        /// <summary>
        /// Data ostatniej próby aktualizacji migracji bazy danych
        /// Date of the last database migration update attempt
        /// </summary>
        [JsonProperty(nameof(LastMigrateDateTime))]
        [Display(Name = "Data ostatniej próby aktualizacji migracji bazy danych", Prompt = "Wpisz lub wybierz datę ostatniej próby aktualizacji migracji bazy danych", Description = "Data ostatniej próby aktualizacji migracji bazy danych")]
        public DateTime LastMigrateDateTime
        {
            get => _lastMigrateDateTime;
            set
            {
                if (value != _lastMigrateDateTime)
                {
                    _lastMigrateDateTime = value;
                    OnPropertyChanged(nameof(LastMigrateDateTime));
                }
            }
        }
        #endregion

        private bool _cheskForConnection;

        #region public bool CheskForConnection { get; set; }
        /// <summary>
        /// Sprawdź możliwość podłączenia do bazy danych\nz wpisanego parametru Ciąg połączenia do bazy danych Mssql
        /// Check the possibility of connecting to the database by entering the Mssql database connection string parameter
        /// </summary>
        [JsonIgnore]
        [Display(Name = "Sprawdź możliwość podłączenia do bazy danych\nz wpisanego parametru Ciąg połączenia do bazy danych Mssql", Prompt = "Zaznacz, jeśli chcesz sprawdzić możliwość podłączenia do bazy danych z wpisanego parametru Ciąg połączenia do bazy danych Mssql", Description = "Sprawdź możliwość podłączenia do bazy danych\nz wpisanego parametru Ciąg połączenia do bazy danych Mssql")]
        public bool CheskForConnection
        {
            get => _cheskForConnection;
            set
            {
                if (value != _cheskForConnection)
                {
                    _cheskForConnection = value;
                    OnPropertyChanged(nameof(CheskForConnection));
                }
            }
        }
        #endregion

        #region private string _connectionString { get; set; }
        /// <summary>
        /// Dostęp prywatny - ciąg połączenia do bazy danych Mssql jako string
        /// Private Access - Mssql database connection string as string
        /// </summary>
        private string _connectionString { get; set; }
        #endregion

        #region public string ConnectionString
        /// <summary>
        /// Dostęp publiczny - ciąg połączenia do bazy danych Mssql jako string
        /// Public Access - Mssql database connection string as a string
        /// </summary>
        [JsonIgnore]
        [Display(Name = "Ciąg połączenia do bazy danych Mssql", Prompt = "Wpisz ciąg połączenia do bazy danych Mssql", Description = "Ciąg połączenia do bazy danych Mssql")]
        [Required]
        [NetAppCommon.Validation.MssqlCanConnect]
        public string ConnectionString
        {
            get => _connectionString;
            set
            {
                _connectionString = value;
                OnPropertyChanged(nameof(ConnectionString));
                if (null != value && !string.IsNullOrWhiteSpace(value))
                {
                    ConnectionStrings = new Dictionary<string, string>
                    {
                        { ConnectionStringName, value }
                    };
                }
            }
        }
        #endregion

        #region public Dictionary<string, string> ConnectionStrings { get; private set; }
        /// <summary>
        /// Słownik zawierający definicję połączenia z nazwąklucza konfiguracji i wartością jako Dictionary
        /// A dictionary containing a connection definition with a configuration key name and value as Dictionary
        /// </summary>
        [JsonProperty(nameof(ConnectionStrings))]
        public Dictionary<string, string> ConnectionStrings { get; private set; }
        #endregion

        #region public string GetConnectionString()
        /// <summary>
        /// Pobierz parametry połączenia
        /// Get the connection string
        /// </summary>
        /// <returns>
        /// Parametry połączenia jako string lub null
        /// Connection string as string or null
        /// </returns>
        public string GetConnectionString()
        {
            try
            {
                return DatabaseMssql.ParseConnectionString(ConnectionString);
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            return null;
        }
        #endregion

        #region public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// PropertyChangedEventHandler PropertyChanged
        /// </summary>
        public new event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        /// <summary>
        /// protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            PropertyChanged?.Invoke(this, args);
        }
        #endregion

        #region private void OnPropertyChanged(string propertyName)
        /// <summary>
        /// private void OnPropertyChanged(string propertyName)
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
    #endregion
}
