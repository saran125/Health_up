using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthUP.Pages.elderly.booking
{
    public class RetrieveModel : PageModel
    {
        [BindProperty]
        public Booking MyBooking { get; set; }


        private readonly BookingService _svc;
        public RetrieveModel(BookingService service)
        {
            _svc = service;
        }

        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                MyBooking = _svc.GetBookingById(id);
                return Page();
            }
            else
                return RedirectToPage("Index");
        }
        //public IActionResult OnPost()
        //{
        //}
    }
}
