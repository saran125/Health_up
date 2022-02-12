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
     
        public Double Rating{ get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string user_id { get; set; }
        public string activity_id { get; set; }
        public string username { get; set; }
        [DataType(DataType.Upload)]
        public string user_photo { get; set; }
        public string activityname { get; set; }
        public string Reply { get; set; }
    }
}
