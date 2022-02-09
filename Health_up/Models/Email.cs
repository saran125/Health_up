using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Models
{
    public class Email
    {
        public string FromMailId { get; set; } = "healthupnyp@gmail.com";
        public string FromMailIdPassword { get; set; } = "He@lthUp123";
        public List<string> ToMailIds { get; set; } = new List<string>();
        public string Subject { get; set; } = "";
        public string Body { get; set; } = "";
        public string IsBodyHtml { get; set; } = "";
        public List<String> Attachments { get; set; } = new List<string>();






    }
}
