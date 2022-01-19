using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.elderly.booking
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;


        private readonly BookingService _svc;
        public DeleteModel(ILogger<DeleteModel> logger, BookingService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public Booking MyBooking { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                MyBooking = _svc.GetBookingById(id);

            }
            else
                return RedirectToPage("Index");

            if (_svc.DeleteBooking(MyBooking))
            {
                return Page();

            }
            else
            {
                MyMessage = "Booking Id does not exist!";
                return Page();
            }
        }
    }
}
