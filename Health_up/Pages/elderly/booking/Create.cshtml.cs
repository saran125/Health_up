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
    public class CreateModel : PageModel

    {
        private readonly ILogger<CreateModel> _logger;
        private BookingService _svc;
        public CreateModel(ILogger<CreateModel> logger, BookingService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public Booking MyBooking { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddBooking(MyBooking))
                {
                    return RedirectToPage("/elderly/booking/ThankYou");

                }
                else
                {
                    MyMessage = "Booking Id already exist!";
                    return Page();
                }

            }
            return Page();
        }
    }
}
