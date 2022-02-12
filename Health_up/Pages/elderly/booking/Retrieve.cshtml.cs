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

namespace HealthUP.Pages.elderly.booking
{
    public class RetrieveModel : PageModel
    {
        [BindProperty]
        public List<Booking> allbookings { get; set; }

        private readonly ILogger<RetrieveModel> _logger;


        private readonly BookingService _svc;
        public RetrieveModel(ILogger<RetrieveModel> logger, BookingService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public Booking MyBooking { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
       

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "elderly")
            {
                allbookings = _svc.GetAllBookings();
                return Page();
            }
            else
            {
                return Redirect("/forbidden");
            }

        }
        //public IActionResult OnPost()
        //{
        //}
    }
}
