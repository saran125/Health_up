using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.elderly.booking
{
    public class ActivityModel : PageModel
    {
        [BindProperty]
        public List<Models.Activity> allactivity { get; set; }

        private readonly ILogger<ActivityModel> _logger;

        private ActivityService _svc;
        public ActivityModel(ILogger<ActivityModel> logger, ActivityService service)
        {
            _logger = logger;
            _svc = service;
        }
        public string ID { get; set; }
        public Models.Activity Myactivity { get; set; }
        public void OnGet()
        {
            allactivity = _svc.AllActivity();
        }
    }
}
