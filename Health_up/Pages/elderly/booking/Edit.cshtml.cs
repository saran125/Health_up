using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages.elderly.booking
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private BookingService _svc;
        private ActivityService _actsv;
        public EditModel(ILogger<EditModel> logger, BookingService service, ActivityService service1)
        {
            _logger = logger;
            _svc = service;
            _actsv = service1;
        }

        [BindProperty]
        public Booking MyBooking { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        [BindProperty]
        public Activity activitydetails { get; set; }
        //public DateTime AvailableDate { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                
                MyBooking = _svc.GetBookingById(id);
                activitydetails = _actsv.GetActivityById(MyBooking.activity_id);

                return Page();
            }
            else
                return RedirectToPage("Index");

        }



        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            activitydetails = _actsv.GetActivityById(MyBooking.activity_id);

            if (_svc.UpdateBooking(MyBooking) == true)
            {
                // save button is clicked
                return RedirectToPage("/elderly/booking/Retrieve");
            }
            else
                return BadRequest();
        }


    }
}
