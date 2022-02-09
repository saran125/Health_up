using System;
using System.Collections.Generic;
using System.Linq;
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
        static async Task Execute()
        {
            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("healthupnyp@gmail.com", "Health Up");
            var subject = "Just checking";
            var to = new EmailAddress("kumarsaran522@gmail.com", "Saran");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.Login(MyUser))
                {
                    if (_svc.CheckEmailVerify(MyUser.Email)){
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
                            return RedirectToPage("/Index");
                        }
                        if (currentuser.Role == "admin")
                        {
                            return RedirectToPage("/Admin");
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
