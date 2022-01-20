using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class DoctorTimeslot
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [RegularExpression(@"^(1[0-2]|0?[1-9]):([0-5]?[0-9]) (AM|PM)$", ErrorMessage = "Invalid Time Format")]
        public string Time { get; set; }

        [Required]
        public string Repeat { get; set; }
    }
}
