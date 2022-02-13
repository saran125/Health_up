using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.admin.Feedback
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;


        private readonly ActivityFeedbackService _svc;
        public DeleteModel(ILogger<DeleteModel> logger, ActivityFeedbackService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public Models.Activity_Feedback Myfeedback { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("Role") == null)
            {
                return Redirect("/forbidden");
            }
            if (id != null)
            {

                Myfeedback = _svc.FeedbackbyId(id);

            }
            if (!_svc.FeedbackExists(id))
            {
                return Redirect("/BadRequest");
            }
            Myfeedback = _svc.FeedbackbyId(id);
            
            if (_svc.DeleteFeedback(Myfeedback))
            {
                if (HttpContext.Session.GetString("Role") == "admin")
                {
                    return Redirect("/admin/Activity/Feedback?Id=" + Myfeedback.activity_id);
                }
                if (HttpContext.Session.GetString("Role") == "elderly")
                {
                    return Redirect("/elderly/booking/Feedback?Id=" + Myfeedback.activity_id);
                }
                return Page();
            }
            else
            {
                MyMessage = "Feedback Id does not exist!";
                return Page();
            }
        }
    }
}
