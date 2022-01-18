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
        public EditModel(ILogger<EditModel> logger, BookingService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public Booking MyBooking { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        //public DateTime AvailableDate { get; set; }

        public void OnGet()
        {
        }
    }
}
