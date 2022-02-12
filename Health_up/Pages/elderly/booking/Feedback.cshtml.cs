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

namespace Health_up.Pages.elderly.booking
{
    public class FeedbackModel : PageModel
    {
        [BindProperty]
        public List<Models.Activity_Feedback> allfeedback{ get; set; }

        private readonly ILogger<FeedbackModel> _logger;

        private ActivityFeedbackService _svc;
        private ActivityService _Activitysvc;
        private UserService _Usersvc;
        public FeedbackModel(ILogger<FeedbackModel> logger, ActivityFeedbackService service, ActivityService activityService, UserService userService)
        {
            _logger = logger;
            _svc = service;
            _Activitysvc = activityService;
            _Usersvc = userService;
        }
        [BindProperty]
        public string ID { get; set; }
        public User theuser { get; set; }
        public string name { get; set; }
        [BindProperty]
        public Models.Activity_Feedback Myfeedback { get; set; }
        [BindProperty]
        public List<Models.RetrieveFeedback> thefeedback { get; set; }
        [BindProperty]
        public Models.RetrieveFeedback feedback { get; set; }

        public IActionResult OnGet(string Id)
        {
            if (Id == null)
            {
                return Redirect("/NotFound");
            }
            ID = Id;
            theuser = _Usersvc.GetUserById(HttpContext.Session.GetString("Email"));
            name = _Activitysvc.GetActivityName(ID);
            allfeedback = _svc.FeedbackbyActivityId(Id);
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                
                return Page();
            }
            else
            {
                Myfeedback.activityname = _Activitysvc.GetActivityName(Myfeedback.activity_id);
                Myfeedback.user_photo = _Usersvc.GetUserById(Myfeedback.user_id).Photo;
                Myfeedback.username = _Usersvc.GetUserById(Myfeedback.user_id).Fname + " " + _Usersvc.GetUserById(Myfeedback.user_id).Lname;
                if (_svc.AddFeedback(Myfeedback))
                {
                    return Redirect("/elderly/booking/Feedback?Id=" + Myfeedback.activity_id);
                }
                else
                    return Page();
            }
        }
    }
}
