using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages.admin
{
    public class hompageModel : PageModel
    {
        private readonly ILogger<hompageModel> _logger;

        private ActivityService _svc;
        private UserService _Usersvc;
        public hompageModel(ILogger<hompageModel> logger, ActivityService service, UserService service1)
        {
            _logger = logger;
            _svc = service;
            _Usersvc = service1;
        }
        public int Users { get; set; }
        public int currentactivity { get; set; }
        public int Pastactivity { get; set; }
        public int upcoming { get; set; }
        public IActionResult OnGet()
        {
            Users = _Usersvc.Users();
            currentactivity = _svc.CurrentActivity();
            Pastactivity = _svc.PastActivity();
            upcoming = _svc.UpcomingActivity();

            return Page();
        }
    }
}
