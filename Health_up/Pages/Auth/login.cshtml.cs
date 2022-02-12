using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using crypto;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HealthUP.Pages
{
    public class loginModel : PageModel
    {
        private readonly ILogger<loginModel> _logger;
        private UserService _svc;
        private IMailService _mailService;
        public loginModel(ILogger<loginModel> logger, UserService service, IMailService IMailService)
        {
            _logger = logger;
            _svc = service;
            _mailService = IMailService;
        }

        [BindProperty]
        public User MyUser { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        public void OnGet()
        {
        }
        public static async Task Execute(string Email, string OTP, string name)
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
            msg.Subject = "Verify your Email address";
            msg.Body = "<strong>Hello " + name + "! Please Verify Your Email!The OTP: " + OTP + "</strong><br/><h3>The OTP will end in 5 mins </h3>";
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
                if (_svc.Login(MyUser))
                {
                    if (_svc.CheckEmailVerify(MyUser.Email))
                    {

                        User currentuser = _svc.Theuser(MyUser);
                        HttpContext.Session.SetString("Email", currentuser.Email);
                        HttpContext.Session.SetString("Fname", currentuser.Fname);
                        HttpContext.Session.SetString("Lname", currentuser.Lname);
                        HttpContext.Session.SetString("Role", currentuser.Role);
                        if (currentuser.Role == "doctor")
                        {
                            return RedirectToPage("/doctor/index");
                        }
                        if (currentuser.Role == "elderly")
                        {
                            return RedirectToPage("/elderly/appointment/Retrieve");
                        }
                        if (currentuser.Role == "admin")
                        {
                            return RedirectToPage("/admin/index");
                        }
                    }
                    else
                    {
                        Random generator = new Random();
                        int r = generator.Next(100000, 1000000);
                        string name = MyUser.Fname + " " + MyUser.Lname;
                        MyUser.OTP = r.ToString();
                        MyUser.OTPTime = DateTime.Now;
                        Execute(MyUser.Email, MyUser.OTP, name).Wait();
                        if (_svc.ResendCode(MyUser))
                        {
                            CookieOptions option = new CookieOptions();
                            option.Expires = DateTime.Now.AddMinutes(5);
                            Response.Cookies.Append("Email", MyUser.Email, option);
                            return RedirectToPage("/Auth/VerifyEmail");
                        }
                    }

                }
                else
                {
                    MyMessage = "Invalid Email or Password";
                    return Page();
                }
            }
           
                return Page();
                    }
    }
}
