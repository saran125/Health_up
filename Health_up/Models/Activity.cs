using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class Activity
    {
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
        public Double Activity_price { get; set; }
        [Required]
        [FileExtensions(Extensions = "jpg, jpeg, png", ErrorMessage = "Provide a valid File Type (JPG/PNG)")]
        [DataType(DataType.Upload)]
        public string Activity_photo { get; set; }
        public string location { get; set; }

    }
   
}
