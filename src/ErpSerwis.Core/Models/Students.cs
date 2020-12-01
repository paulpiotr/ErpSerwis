using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;

namespace ErpSerwis.Core.Models
{
    [Table("Students", Schema = "esc")]
    public class Students : IEditableObject, INotifyPropertyChanged
    {
        #region public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// PropertyChangedEventHandler PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public static readonly string DateTimeMin = DateTime.Now.ToString();

        private Guid _id;

        #region public Guid Id
        /// <summary>
        /// Identyfikator (klucz główny)
        /// Identifier (master key)
        /// </summary>
        [Key]
        [Editable(false)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
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

        private string _name;

        #region public string Name
        [JsonProperty(nameof(Name))]
        [Column("Name", TypeName = "varchar(32)")]
        [Display(Name = "Imię", Prompt = "Wpisz imię", Description = "Imię")]
        [Required]
        [StringLength(32)]
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        #endregion

        private string _surname;

        #region public string Surname
        [JsonProperty(nameof(Surname))]
        [Column("Surname", TypeName = "varchar(32)")]
        [Display(Name = "Nazwisko", Prompt = "Wpisz nazwisko", Description = "Nazwisko")]
        [Required]
        [StringLength(32)]
        public string Surname
        {
            get => _surname;
            set
            {
                if (value != _surname)
                {
                    _surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }
        #endregion

        private string _indexNumber;

        #region public string IndexNumber
        [JsonProperty(nameof(IndexNumber))]
        [Column("IndexNumber", TypeName = "varchar(32)")]
        [Display(Name = "Numer indeksu", Prompt = "Wpisz numer indeksu", Description = "Numer indeksu")]
        [Required]
        [StringLength(32)]
        public string IndexNumber
        {
            get => _indexNumber;
            set
            {
                if (value != _indexNumber)
                {
                    _indexNumber = value;
                    OnPropertyChanged("IndexNumber");
                }
            }
        }
        #endregion

        private DateTime _dateOfBirth = DateTime.Now;

        #region public DateTime DateOfBirth
        /// <summary>
        /// Data urodzenia
        /// Date of birth
        /// </summary>
        [JsonProperty(nameof(DateOfBirth))]
        [Column("DateOfBirth", TypeName = "datetime")]
        [Display(Name = "Data urodzenia", Prompt = "Wpisz lub wybierz datę urodzenia", Description = "Data urodzenia")]
        [Required]
        [DataType(DataType.Date)]
        [NetAppCommon.Validation.DateYearsRange(-120, -18)]
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (value != _dateOfBirth)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }
        #endregion

        private ICollection<StudentsGrades> _studentsGrades;

        #region public virtual ICollection<StudentsGrades> StudentsGrades { get; set; }
        /// <summary>
        /// Obiekt importowanego pliku XML jako FileImportStatusHistoryFromIcasa
        /// The imported XML file object as FileImportStatusHistoryFromIcasa
        /// </summary>
        [Browsable(false)]
        [Display(AutoGenerateField = false)]
        [InverseProperty("Students")]
        public virtual ICollection<StudentsGrades> StudentsGrades
        {
            get => _studentsGrades;
            set
            {
                if (value != _studentsGrades)
                {
                    _studentsGrades = value;
                    OnPropertyChanged("StudentsGrades");
                }
            }
        }
        #endregion

        private string _studentsGradesToString;

        [NotMapped]
        [Display(Name = "Oceny studenta", Prompt = "Wpisz oceny studenta rozdzielone przecinkiem", Description = "Oceny studenta")]
        public string StudentsGradesToString
        {
            get
            {
                if (null != StudentsGrades && StudentsGrades.Count > 0)
                {
                    _studentsGradesToString = string.Join(", ", StudentsGrades.Select(x => x.Grade).ToArray());
                }
                return _studentsGradesToString;
            }
        }

        public void BeginEdit()
        {
        }

        public void CancelEdit()
        {
        }

        public void EndEdit()
        {
        }

        public void EndAddGrade()
        {
            OnPropertyChanged(nameof(StudentsGrades));
        }

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
