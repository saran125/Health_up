using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class MedicalReport
    {
        [Required]
        [Key]
        public string Report_id { get; set; }
        [Required]
        public DateTime Report_date { get; set; }
        [Required]
        public bool Signature { get; set; }
        public long Note { get; set; }
        [Required]
        public string DoctorNRIC { get; set; }
        [Required]
        public string ElderlyNRIC { get; set; }

    }
}
