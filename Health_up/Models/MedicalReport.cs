using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class MedicalReport
    {
        [Key]
        public string Report_id { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Report_date { get; set; }
        [Required]
        public bool Signature { get; set; }
        [Required]
        public string Symptoms { get; set; }
        [Required]
        public string Immunities { get; set; }
        [Required]
        public string Medicines { get; set; }
        [Required]
        public double Medicine_Cost { get; set; }
        public string Note { get; set; }
        [Required]
        public string Doctor_ID { get; set; }
        [Required]
        public string Elderly_ID { get; set; }

    }
}
