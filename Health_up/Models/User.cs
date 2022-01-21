using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class User
    {

     
        [Key]
        public string NRIC { get; set; }

        [ MaxLength(25)]
        public string Fname { get; set; }
        [ MaxLength(25)]
        public string Lname { get; set; }
        
        public string Email { get; set; }
        
        public string Password { get; set; }
       
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }
        public string PostalCode { get; set; }
        
        public string UnitNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public string Specialisation { get; set; }
        public string HospitalAddress { get; set; }
        public string Verify { get; set; }

    }
}
