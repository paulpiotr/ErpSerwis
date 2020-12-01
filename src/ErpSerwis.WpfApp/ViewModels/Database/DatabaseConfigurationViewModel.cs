using System.Collections.ObjectModel;
using System.ComponentModel;
using ErpSerwis.Core.Models;
using Telerik.Windows.Data;

namespace ErpSerwis.WpfApp.ViewModels.Database
{
    public class DatabaseConfigurationViewModel : INotifyPropertyChanged
    {
        #region private ICollectionView _databaseConfigurationCollectionView;
        /// <summary>
        /// private ICollectionView _databaseConfigurationCollectionView;
        /// </summary>
        private ICollectionView _databaseConfigurationCollectionView;
        #endregion

        #region public ICollectionView DatabaseConfigurationCollectionView
        /// <summary>
        /// public ICollectionView DatabaseConfigurationCollectionView
        /// </summary>
        public ICollectionView DatabaseConfigurationCollectionView
        {
            get
            {
                if (null == _databaseConfigurationCollectionView)
                {
                    var completeDatabaseModel = new ObservableCollection<AppSettings>
                    {
                        new AppSettings(),
                    };
                    _databaseConfigurationCollectionView = new QueryableCollectionView(completeDatabaseModel);
                }
                return _databaseConfigurationCollectionView;
            }
            set
            {
                _databaseConfigurationCollectionView = value;
                OnPropertyChanged(nameof(DatabaseConfigurationCollectionView));
            }
        }
        #endregion

        #region public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// public event PropertyChangedEventHandler PropertyChanged;
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
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
}
