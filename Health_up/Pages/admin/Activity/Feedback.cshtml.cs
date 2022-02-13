using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.admin.Activity
{
    public class FeedbackModel : PageModel
    {
        [BindProperty]
        public List<Models.Activity_Feedback> allfeedback { get; set; }

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
        public string name { get; set; }
        [BindProperty]
        public Models.Activity_Feedback Myfeedback { get; set; }
        [BindProperty]
        public List<Models.RetrieveFeedback> thefeedback { get; set; }
        [BindProperty]
        public Models.RetrieveFeedback feedback { get; set; }

        public IActionResult OnGet(string Id)
        {
            if (HttpContext.Session.GetString("Role") != "admin")
            {
                return Redirect("/forbidden");
            }
            if (Id == null)
            {
                return Redirect("/NotFound");
            }
            if (!_Activitysvc.ActivityExists(Id))
            {
                return Redirect("/BadRequest");
            }
            name = _Activitysvc.GetActivityName(Id);
            ID = Id;
            allfeedback = _svc.FeedbackbyActivityId(Id);
            return Page();
        }
    }
}
