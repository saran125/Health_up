using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.admin.Users
{
    public class Email_EveryoneModel : PageModel
    {
        private readonly ILogger<Email_EveryoneModel> _logger;
        private UserService _Usersvc;
        private ActivityService _svc;
        private BookingService _booking;
        public Email_EveryoneModel(ILogger<Email_EveryoneModel> logger, ActivityService service, UserService service1, BookingService booking)
        {
            _logger = logger;
            _Usersvc = service1;
            _svc = service;
            _booking = booking;
        }
        [BindProperty]
        public User theuser { get; set; }
        [BindProperty]
        public Health_up.Models.Activity theactivity { get; set; }
        [BindProperty]
        public string subject { get; set; }
        [BindProperty]
        public string message { get; set; }
        [BindProperty]
        public string email { get; set; }
        public IActionResult OnGet(string Id)
        {
            if (HttpContext.Session.GetString("Role") == "admin")
            {
                if (!_svc.ActivityExists(Id))
                {
                    return Redirect("/BadRequest");
                }
                theactivity = _svc.GetActivityById(Id);
                foreach (var i in _Usersvc.GetTheElderly())
                {
                    email = i.Email;
                    message = "Dear " + i.Fname + " " + i.Lname + ", We have an Interesting Activity For You to take part!" +" The activity is " + theactivity.Activity_name+ ". It is from " + theactivity.Activity_start_date.ToShortDateString() + " to " +theactivity.Activity_end_date.ToShortDateString() + ". The time is " + theactivity.Activity_timeslot+". For more details, visit our website!" ;
                    subject = "Interesting Activtiy";
                    Execute(email, message, subject).Wait();
                }
                return Redirect("/admin/Activity/Success");
            }
            else
            {
                return Redirect("/forbidden");
            }
        }
        public static async Task Execute(string Email, string Message, string subject)
        {

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("healthupnyp@gmail.com", "He@lth1234");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = subject;
            msg.Body = Message;
            string toaddress = Email;
            msg.To.Add(toaddress);
            string fromaddress = "HealthUp <healthupnyp@gmail.com>";
            msg.From = new MailAddress(fromaddress);
            try
            {
                smtp.Send(msg);
            }
            catch
            {
                throw;
            }

        }
    }
}
