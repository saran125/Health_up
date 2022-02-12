using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.admin.Users
{
    public class ParticipantsModel : PageModel
    {
        private readonly ILogger<ParticipantsModel> _logger;
        private BookingService _bookingsvc;
        private ActivityService _svc;
        private UserService _Usersvc;
        public ParticipantsModel(ILogger<ParticipantsModel> logger, ActivityService service, UserService service1, BookingService bookingService)
        {
            _logger = logger;
            _svc = service;
            _Usersvc = service1;
            _bookingsvc = bookingService;
        }
        public int Users { get; set; }
        public int Doctor { get; set; }
        public int Elderly { get; set; }
        public List<Health_up.Models.User> alluser { get; set; }
        public List<Health_up.Models.Booking> Allbookings { get; set; }
        public Models.Activity theactivity { get; set; }
        public Models.User theuser { get; set; }

        public IActionResult OnGet(String Id)
        {

            theactivity = _svc.GetActivityById(Id);
            Allbookings = _bookingsvc.GetByActivityBookings(Id);
            foreach (var i in Allbookings)
            {
                theuser = _Usersvc.GetUserById(i.elderly_id);
                theuser.BirthDate = i.Date;
                alluser.Add(theuser);
            }
            return Page();
        }
    }
}
