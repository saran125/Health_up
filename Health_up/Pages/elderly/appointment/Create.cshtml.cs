using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages.elderly.appointments
{
    public class CreateModel : PageModel

    {
        private readonly ILogger<CreateModel> _logger;
        private AppointmentService _svc;
        public CreateModel(ILogger<CreateModel> logger, AppointmentService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public Appointment MyAppointment { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }


        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddAppointment(MyAppointment))
                {
                    return RedirectToPage("/elderly/appointment/ThankYou");

                }
                else
                {
                    MyMessage = "Appointment Id already exist!";
                    return Page();
                }

            }
            return Page();
        }
    }
}
