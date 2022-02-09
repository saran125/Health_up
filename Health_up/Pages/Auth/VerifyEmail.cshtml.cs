using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Health_up.Pages.Auth
{


   
    public class VerifyEmailModel : PageModel
    {

        private readonly ILogger<VerifyEmailModel> _logger;
        private UserService _svc;
        public VerifyEmailModel(ILogger<VerifyEmailModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }
        [BindProperty]
        public User MyUser { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        [BindProperty]
        public string Email { get; set; }
        public IActionResult OnGet()
        {
            if (Request.Cookies["Email"] == null)
            {
                return Redirect("/forbidden");
            }
            else
            {
                Email = Request.Cookies["Email"];
            }
            return Page();
        }
        public static async Task Execute(string Email, string OTP, string name)
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("healthupnyp@gmail.com", "Health Up");
            var subject = "Verify Email";
            var to = new EmailAddress(Email, name);
            var plainTextContent = "Hello " + name + "! Please Verify Your Email! The OTP:" + OTP;
            var htmlContent = "<strong>Hello " + name + "! Please Verify Your Email!The OTP: " + OTP + "</strong><br/><h3>The OTP will end in 5 mins </h3>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
        public IActionResult Resend(string button)
        {
            Random generator = new Random();
            int r = generator.Next(100000, 1000000);
            string name = MyUser.Fname + " " + MyUser.Lname;
            MyUser.OTP = r.ToString();
            MyUser.OTPTime = DateTime.Now;
            Execute(MyUser.Email, MyUser.OTP, name).Wait();
            _svc.Register(MyUser);
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddMinutes(5);
                Response.Cookies.Append("Email", MyUser.Email, option);
                return RedirectToPage("/Auth/VerifyEmail");
            

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.Verify(MyUser))
                {
                    return Redirect("/Auth/EmailVerified");
                }
                else
                {
                    MyMessage = "Invalid Code!";
                    return Page();
                }
               
            }
            return Page();
        }
    }
}
