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
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;


        private readonly UserService _svc;
        public DeleteModel(ILogger<DeleteModel> logger, UserService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public User theuser { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        public static async Task Execute(string Email, string name)
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
            msg.Subject = "Your Account Is Removed!";
            msg.Body = "Hello " + name + "! We Have Removed Your Account From Accessing Our service! " +
                "Contact us for any clarification. Email: healthupnyp@gmail.com";
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

                theuser = _svc.GetUserById(id);
             
                string name = theuser.Fname + " " + theuser.Lname;
              
                Execute(theuser.Email, name).Wait();
                if (_svc.DeleteUser(id))
                    {
                        return RedirectToPage("/admin/Users/users");
                    }
                    else
                    {
                        MyMessage = "Users does not exist!";
                        return Page();
                    }
                
            }
            else
                return Redirect("/forbidden");
        }
    }
}
