using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.admin.Activity
{
    public class ReplyModel : PageModel
    {
        [BindProperty]
        public List<Models.Activity_Feedback> allfeedback { get; set; }

        private readonly ILogger<ReplyModel> _logger;

        private ActivityFeedbackService _svc;
        private ActivityService _Activitysvc;
        private UserService _Usersvc;
        public ReplyModel(ILogger<ReplyModel> logger, ActivityFeedbackService service, ActivityService activityService, UserService userService)
        {
            _logger = logger;
            _svc = service;
            _Activitysvc = activityService;
            _Usersvc = userService;
        }
        [BindProperty]
        public string ID { get; set; }
        public string name { get; set; }
        [BindProperty]
        public Models.Activity_Feedback Myfeedback { get; set; }
        [BindProperty]
        public List<Models.RetrieveFeedback> thefeedback { get; set; }
        [BindProperty]
        public Models.RetrieveFeedback feedback { get; set; }
        [BindProperty]

        public string Subject { get; set; }
        public IActionResult OnGet(string Id)
        {
            if (HttpContext.Session.GetString("Role") == "admin")
            {
                Myfeedback = _svc.FeedbackbyId(Id);
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
                if (_svc.ReplyFeedback(Myfeedback))
                {
                        Execute(Myfeedback.user_id, Myfeedback.Reply   ,Subject).Wait();
                    return Redirect("/admin/Activity/Success");
                }
                else
                {
                 
                    return Page();
                }
            }

            return Page();
        }


    }
}
