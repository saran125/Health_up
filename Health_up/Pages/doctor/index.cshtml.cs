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

namespace Health_up.Pages.doctor
{
    public class indexModel : PageModel
    {
        /*
        private readonly ILogger<indexModel> _logger;
        private AppointmentService _svc;
        public indexModel(ILogger<indexModel> logger, AppointmentService service)
        {
            _logger = logger;
            _svc = service;
        }
        [BindProperty]
        public Appointment appointment { get; set; }
        */
        public void OnGet()
        {
            /*
            if(HttpContext.Session.GetString("Role") == "doctor")
            {
                string email = HttpContext.Session.GetString("Email");
                List<Appointment> docappts = _svc.GetAllAppointments().Where(e => e.DoctorId == email).ToList();

            }
            else
            {
                RedirectToPage("/Auth/login");

            }

            */

        }
        public IActionResult OnPost()
        {
            return RedirectToPage("report/create");
        }
    }
}
