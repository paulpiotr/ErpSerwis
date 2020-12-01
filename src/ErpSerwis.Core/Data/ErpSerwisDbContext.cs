using System;
using System.Reflection;
using ErpSerwis.Core.Models;
using Microsoft.EntityFrameworkCore;
using NetAppCommon;

namespace ErpSerwis.Core.Data
{
    public partial class ErpSerwisDbContext : DbContext
    {
        #region private static readonly log4net.ILog Log4net
        /// <summary>
        /// Log4 Net Logger
        /// </summary>
        private static readonly log4net.ILog Log4net = Log4netLogger.Log4netLogger.GetLog4netInstance(MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        #region private static readonly AppSettings appSettings...
        /// <summary>
        /// Instancja do klasy modelu ustawień jako AppSettings
        /// Instance to the settings model class as AppSettings
        /// </summary>
        private readonly AppSettings _appSettings;
        #endregion

        public ErpSerwisDbContext()
        {
            _appSettings = AppSettings.GetInstance();
            //CheckForUpdateAndCreateMssqlMdf();
        }

        public ErpSerwisDbContext(DbContextOptions<ErpSerwisDbContext> options)
            : base(options)
        {
            _appSettings = AppSettings.GetInstance();
            //CheckForUpdateAndCreateMssqlMdf();
        }

        #region public void CheckForUpdateAndMigrate(AppSettings appSettings = null)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        public bool CheckForUpdateAndMigrate(AppSettings appSettings = null)
        {
            var isCheckForUpdateAndMigrate = false;
            appSettings ??= _appSettings;
            try
            {
                var result = (DateTime.Now - appSettings.LastMigrateDateTime).Days;
                Log4net.Debug($"CheckForUpdateAndMigrate, compare { DateTime.Now } and { appSettings.LastMigrateDateTime } is { result } CheckForUpdateEveryDays is { appSettings.CheckForUpdateEveryDays }");
                if (result >= appSettings.CheckForUpdateEveryDays)
                {
                    Database.Migrate();
                    isCheckForUpdateAndMigrate = true;
                }
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            finally
            {
                if (null != appSettings)
                {
                    appSettings.LastMigrateDateTime = DateTime.Now;
                    Repository.AppSettingsRepository.GetInstance().Save(appSettings);
                }
            }
            return isCheckForUpdateAndMigrate;
        }
        #endregion

        #region public void CheckForUpdateAndCreateMssqlMdf(AppSettings appSettings = null)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        public bool CheckForUpdateAndCreateMssqlMdf(AppSettings appSettings = null)
        {
            var isCheckForUpdateAndCreateMssqlMdfResult = false;
            appSettings ??= _appSettings;
            try
            {
                var result = (DateTime.Now - appSettings.LastMigrateDateTime).Days;
                Log4net.Debug($"CheckForUpdateAndCreateMssqlMdf, compare { DateTime.Now } and { appSettings.LastMigrateDateTime } is { result } CheckForUpdateEveryDays is { appSettings.CheckForUpdateEveryDays }");
                if (result >= appSettings.CheckForUpdateEveryDays)
                {
                    DatabaseMssqlMdf.GetInstance(Database.GetDbConnection().ConnectionString).Create();
                    isCheckForUpdateAndCreateMssqlMdfResult = true;
                }
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
            finally
            {
                if (null != appSettings)
                {
                    appSettings.LastMigrateDateTime = DateTime.Now;
                    Repository.AppSettingsRepository.GetInstance().Save(appSettings);
                }
            }
            return isCheckForUpdateAndCreateMssqlMdfResult;
        }
        #endregion

        #region public virtual DbSet<Students> Students { get; set; }
        /// <summary>
        /// Model danych Students
        /// Students data model
        /// </summary>
        public virtual DbSet<Students> Students { get; set; }
        #endregion

        #region public virtual DbSet<StudentsGrades> StudentsGrades { get; set; }
        /// <summary>
        /// Model danych StudentsGrades
        /// StudentsGrades data model
        /// </summary>
        public virtual DbSet<StudentsGrades> StudentsGrades { get; set; }
        #endregion

        #region protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        /// <summary>
        /// Zdarzenie wyzwalające konfigurację bazy danych
        /// Database configuration triggering event
        /// </summary>
        /// <param name="optionsBuilder">
        /// Fabryka budowania połączenia do bazy danych optionsBuilder jako DbContextOptionsBuilder
        /// Factory building connection to the database optionsBuilder AS DbContextOptionsBuilder
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                //#if DEBUG
                //                ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
                //                {
                //                    builder.AddFilter(level => level == LogLevel.Debug).AddConsole();
                //                });
                //                optionsBuilder.UseLoggerFactory(loggerFactory);
                //#endif
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(_appSettings.GetConnectionString());
                }
            }
            catch (Exception e)
            {
                Log4net.Error(string.Format("\n{0}\n{1}\n{2}\n{3}\n", e.GetType(), e.InnerException?.GetType(), e.Message, e.StackTrace), e);
            }
        }
        #endregion

        #region protected override void OnModelCreating(ModelBuilder modelBuilder)
        /// <summary>
        /// Zdarzenie wyzwalające tworzenie modelu bazy danych
        /// The event that triggers the creation of the database model
        /// </summary>
        /// <param name="modelBuilder">
        /// Fabryka budowania modelu bazy danych modelBuilder jako ModelBuilder
        /// ModelBuilder database model building as ModelBuilder
        /// </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentsConfiguration());
            modelBuilder.ApplyConfiguration(new StudentsGradesConfiguration());
        }
        #endregion

        #region partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        /// <summary>
        /// OnModelCreatingPartial
        /// </summary>
        /// <param name="modelBuilder">
        /// ModelBuilder modelBuilder
        /// </param>
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        #endregion
    }
}
