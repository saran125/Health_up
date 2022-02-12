using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class Booking
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string TimeSlot { get; set; }

        [Required]
        public string Location { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public double ActivityPrice { get; set; }
        public string Elderly_Id { get; set; }





    }
}
