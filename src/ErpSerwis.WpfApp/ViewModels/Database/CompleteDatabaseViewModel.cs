using System.Collections.ObjectModel;
using System.ComponentModel;
using ErpSerwis.Core.Models.Database;
using Telerik.Windows.Data;

namespace ErpSerwis.WpfApp.ViewModels.Database
{
    public class CompleteDatabaseViewModel : INotifyPropertyChanged
    {
        #region private ICollectionView _completeDatabaseModel;
        /// <summary>
        /// private ICollectionView _completeDatabaseModel;
        /// </summary>
        private ICollectionView _completeDatabaseModel;
        #endregion

        #region public ICollectionView CompleteDatabaseModel
        /// <summary>
        /// public ICollectionView CompleteDatabaseModel
        /// </summary>
        public ICollectionView CompleteDatabaseModel
        {
            get
            {
                if (null == _completeDatabaseModel)
                {
                    var completeDatabaseModel = new ObservableCollection<CompleteDatabaseModel>
                    {
                        new CompleteDatabaseModel { NumberOfRecords = 100, ConfirmTheOperation = false }
                    };
                    _completeDatabaseModel = new QueryableCollectionView(completeDatabaseModel);
                }
                return _completeDatabaseModel;
            }
            set
            {
                _completeDatabaseModel = value;
                OnPropertyChanged(nameof(CompleteDatabaseModel));
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
