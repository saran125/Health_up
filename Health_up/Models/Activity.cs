using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class Activity
    {
        [Required]
        public string Id { get; set; }

        [Required, MaxLength(25)]
        public string Activity_name { get; set; }

        [Required]
        public string Activity_description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Activity_start_date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Activity_end_date { get; set; }
        [Required]
        public string Activity_timeslot { get; set; }
        [Required]
        public string Activity_price { get; set; }
        [Required]
        public double Activity_photo { get; set; }

    }
}
