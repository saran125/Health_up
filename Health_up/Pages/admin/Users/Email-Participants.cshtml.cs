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
    public class Email_ParticipantsModel : PageModel
    {
        private readonly ILogger<Email_ParticipantsModel> _logger;


        private UserService _Usersvc;
        private ActivityService _svc;
        private BookingService _booking;
        public Email_ParticipantsModel(ILogger<Email_ParticipantsModel> logger, ActivityService service, UserService service1, BookingService booking)
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
                
                return Page();
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

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                foreach (var i in _booking.GetByActivityBookings(theactivity.Id)){

                    email = i.elderly_id;
                    Execute(email, message, subject).Wait();
                }
                return Redirect("/admin/Activity/Success");

            }

            return Page();
        }
    }
}
