using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ErpSerwis.Core.Models
{
    [Table("StudentsGrades", Schema = "esc")]
    public class StudentsGrades : INotifyPropertyChanged
    {
        #region public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// PropertyChangedEventHandler PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        private Guid _id;

        #region public Guid Id
        /// <summary>
        /// Identyfikator (klucz główny)
        /// Identifier (master key)
        /// </summary>
        [Key]
        [JsonProperty(nameof(Id))]
        [Display(Name = "Identyfikator", Prompt = "Wpisz identyfikator (klucz główny)", Description = "Identyfikator (klucz główny)")]
        public Guid Id
        {
            get => _id;
            set
            {
                if (value != _id)
                {
                    _id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        #endregion

        private Guid _studentId;

        #region public Guid StudentId
        [JsonProperty(nameof(StudentId))]
        [Display(Name = "Identyfikator studenta", Prompt = "Wybierz studenta", Description = "Identyfikator studenta")]
        [Required]
        public Guid StudentId
        {
            get => _studentId;
            set
            {
                if (value != _studentId)
                {
                    _studentId = value;
                    OnPropertyChanged("StudentId");
                }
            }
        }
        #endregion

        private decimal _grade;

        #region public string Grade
        [JsonProperty(nameof(Grade))]
        [Column("Grade", TypeName = "money")]
        [Display(Name = "Ocena", Prompt = "Wpisz ocenę", Description = "Ocena")]
        [Required]
        [Range((double)decimal.MinValue, (double)decimal.MaxValue)]
        public decimal Grade
        {
            get => _grade;
            set
            {
                if (value != _grade)
                {
                    _grade = value;
                    OnPropertyChanged("Grade");
                }
            }
        }
        #endregion

        #region public virtual Students Students { get; set; }
        /// <summary>
        /// to do
        /// </summary>
        [ForeignKey(nameof(StudentId))]
        [InverseProperty(nameof(Models.Students.StudentsGrades))]
        public virtual Students Students { get; set; }
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
