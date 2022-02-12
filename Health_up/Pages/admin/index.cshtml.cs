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

namespace HealthUP.Pages.admin
{
    public class hompageModel : PageModel
    {
        private readonly ILogger<hompageModel> _logger;

        private ActivityService _svc;
        private UserService _Usersvc;

       
        private BookingService _bookingsvc;
        public hompageModel(ILogger<hompageModel> logger, ActivityService service, UserService service1,  BookingService bookingService)
        {
            _logger = logger;
            _svc = service;
            _Usersvc = service1;

            _bookingsvc = bookingService;
        }
        public int Users { get; set; }
        public int currentactivity { get; set; }
        public int Pastactivity { get; set; }
        public int upcoming { get; set; }
        public List<string> Activity_Name {get;set;}
        public List<Health_up.Models.Activity> Activities { get; set; }
        public List<int> values { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") != "admin")
            {
                return Redirect("/forbidden");
            }
            List<string> names = new List<string>();
            List<int> participants = new List<int>();
            Users = _Usersvc.Users();
            currentactivity = _svc.CurrentActivity();
            Pastactivity = _svc.PastActivity();
            upcoming = _svc.UpcomingActivity();
            Activities = _svc.Activity();
           foreach(var i in _svc.Activity())
            {
                names.Add(i.Activity_name);
            }
            int count = 0;
           foreach(var e in Activities)
            {
                count = 0;
                foreach(var i in _bookingsvc.GetAllBookings())
                {
                    if(i.activity_id == e.Id)
                    {
                        count += 1;
                    }
                }
                participants.Add(count);
            }
            values = participants;
            Activity_Name = names;
             return Page();
        }
    }
}
