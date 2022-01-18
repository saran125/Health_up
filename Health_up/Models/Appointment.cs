using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class Appointment
    {
        [Required]
        public string Id { get; set; }

        [Required, MaxLength(25)]
        public string Symptoms { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime AvailableDate { get; set; }
        [Required]
        public string AvailableTimeslot { get; set; }
        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        public double ConsultationFee { get; set; }

    }
}
