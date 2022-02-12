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
    public class PastActivityModel : PageModel
    {

        [BindProperty]
        public List<Models.Activity> allactivity { get; set; }

        private readonly ILogger<PastActivityModel> _logger;

        private ActivityService _svc;
        public PastActivityModel(ILogger<PastActivityModel> logger, ActivityService service)
        {
            _logger = logger;
            _svc = service;
        }
        public string ID { get; set; }
        public Models.Activity Myactivity { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") != "admin")
            {
                return Redirect("/forbidden");
            }
                allactivity = _svc.AllPastActivity();
            return Page();
        }
        public IActionResult OnPost()
        {
            if (_svc.DeleteActivity(Myactivity) == true)
            {
                return RedirectToPage("/admin/Activity/View");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
