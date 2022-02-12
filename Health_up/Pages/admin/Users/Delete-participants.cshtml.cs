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
    public class Delete_participantsModel : PageModel
    {
        private readonly ILogger<Delete_participantsModel> _logger;


        private readonly BookingService _svc;
        private readonly UserService _Usersvc;
        private readonly ActivityService _Activitysvc;
        public Delete_participantsModel(ILogger<Delete_participantsModel> logger, BookingService service, UserService service1, ActivityService service2)
        {
            _logger = logger;
            _Usersvc = service1;
            _Activitysvc = service2;
            _svc = service;
        }
        [BindProperty]
        public Booking thebooking { get; set; }
        [BindProperty]
        public User theuser { get; set; }
       [BindProperty]
       public Models.Activity theactivity { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        public static async Task Execute(string Email, string name, string activity)
        {
            /* var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("healthupnyp@gmail.com", "Health Up");
            var subject = "Verify Email";
            var to = new EmailAddress(Email, name);
            var plainTextContent = "Hello " + name + "! Please Verify Your Email! The OTP:" + OTP;
            var htmlContent = "<strong>Hello " + name + "! Please Verify Your Email!The OTP: " + OTP + "</strong><br/><h3>The OTP will end in 5 mins </h3>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
             */
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("healthupnyp@gmail.com", "He@lth1234");
            smtp.EnableSsl = true;
            MailMessage msg = new MailMessage();
            msg.Subject = "Removed from taking part!";
            msg.Body = "Hello " + name + "! We Have Removed You from taking in " + activity+
                ". Contact us for any clarification. Email: healthupnyp@gmail.com";
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
        public IActionResult OnGet(string id)
        {
            if ((HttpContext.Session.GetString("Role") == "admin"))
            {

                thebooking = _svc.GetBookingById(id);
                theuser = _Usersvc.GetUserById(thebooking.elderly_id);
                string name = theuser.Fname + " " + theuser.Lname;
                var msg = _Activitysvc.GetActivityById(thebooking.activity_id).Activity_name;
                Execute(thebooking.elderly_id, name, msg).Wait();
                if (_svc.DeleteBooking(thebooking))
                {
                    return RedirectToPage("/Index");
                }
                else
                {
                    MyMessage = "Booking Id does not exist!";
                    return Page();
                }

            }
            else
                return Redirect("/forbidden");
        }
    }
}
