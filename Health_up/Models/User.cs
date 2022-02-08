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
        [EmailAddress]
        public string Email { get; set; }


        [ MaxLength(25)]
        public string Fname { get; set; }
        [ MaxLength(25)]
        public string Lname { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
          ErrorMessage = "Password need to be at least 8 characters, combination of lower case, upper case, numbers & special characters")]
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
