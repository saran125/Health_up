using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.elderly.appointment
{
    public class DetailsModel : PageModel
    {
        private readonly ILogger<DetailsModel> _logger;


        private readonly AppointmentService _svc;

        public DetailsModel(ILogger<DetailsModel> logger, AppointmentService service)
        {
            _logger = logger;

            _svc = service;

        }
        [BindProperty]
        public Appointment MyAppointment { get; set; }


        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                MyAppointment = _svc.GetAppointmentById(id);

                return Page();
            }
            else
                return RedirectToPage("/elderly/404");

        }
    }
}
