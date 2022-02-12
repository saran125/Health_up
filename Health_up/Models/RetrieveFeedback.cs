using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class RetrieveFeedback
    {
        public string Id { get; set; }
        public string Comments { get; set; }
        public double Rating{ get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string username { get; set; }
        [DataType(DataType.Upload)]
        public string user_photo { get; set; }
        public string activityname { get; set; }
    }
}
