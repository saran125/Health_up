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

namespace Health_up.Pages.elderly.booking
{
    public class HistoryModel : PageModel
    {
        [BindProperty]
        public List<Booking> bookingByEmail { get; set; }

        private readonly ILogger<HistoryModel> _logger;


        private readonly BookingService _svc;
        public HistoryModel(ILogger<HistoryModel> logger, BookingService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public Booking MyBoooking { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        public void OnGet()
        {
            bookingByEmail = _svc.GetBookingByEmail(HttpContext.Session.GetString("Email"));

        }
    }
}
