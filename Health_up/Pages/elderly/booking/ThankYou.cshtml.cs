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
    public class ThankYouModel : PageModel
    {

        private readonly ILogger<ThankYouModel> _logger;
        private BookingService _svc;
        public ThankYouModel(ILogger<ThankYouModel> logger, BookingService service)
        {
            _logger = logger;
            _svc = service;
        }

        public void OnGet()
        {

        }
        //public IActionResult OnPost()
        //{


        //}

    }
}
