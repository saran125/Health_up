using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
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
        [BindProperty]
        public List<Health_up.Models.User> alluser { get; set; }
        [BindProperty]
        public List<Health_up.Models.Booking> Allbookings { get; set; }
        [BindProperty]
        public Models.Activity theactivity { get; set; }
        [BindProperty]
        public Models.User theuser { get; set; }

        public void OnGet(String Id)
        {

            List<User> AllTheUser = new List<User>();
            theactivity = _svc.GetActivityById(Id);
            Allbookings = _bookingsvc.GetByActivityBookings(Id);
            foreach (var i in Allbookings)
            {
                theuser = _Usersvc.GetUserById(i.elderly_id);
                 theuser.BirthDate = i.Date;
                theuser.HospitalAddress = i.Id;

                AllTheUser.Add(theuser);
              
            }
            alluser = AllTheUser;
            
        }
    }
}
