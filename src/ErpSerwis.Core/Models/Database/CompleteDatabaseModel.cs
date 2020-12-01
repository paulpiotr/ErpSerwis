using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ErpSerwis.Core.Models.Database
{
    [NotMapped]
    public class CompleteDatabaseModel
    {
        private readonly int _numberOfRecords;

        [JsonProperty(nameof(NumberOfRecords))]
        [Display(Name = "Ilość rekordów do wstawienia", Prompt = "Wpisz ilość rekordów do wstawienia", Description = "Ilość rekordów do wstawienia")]
        [Range(int.MinValue, int.MaxValue)]
        [Required]
        public int NumberOfRecords { get; set; }

        private readonly int _confirmTheOperation;

        [JsonProperty(nameof(NumberOfRecords))]
        [Display(Name = "Potwierdź operację", Prompt = "Zaznacz, potwierdź operację", Description = "Potwierdź operację")]
        [Required]
        public bool ConfirmTheOperation { get; set; }
    }
}
