using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class Activity_Feedback
    {
        public string Id { get; set; }

       
        public string Comments { get; set; }


        public string Rating{ get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string user_id { get; set; }
        public string activity_id { get; set; }
    }
}
