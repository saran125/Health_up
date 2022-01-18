using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages.elderly.appointment
{
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private AppointmentService _svc;
        public EditModel(ILogger<EditModel> logger, AppointmentService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public Appointment MyAppointment { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        //public DateTime AvailableDate { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                MyAppointment = _svc.GetAppointmentById(id);

                return Page();
            }
            else
                return RedirectToPage("Index");

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateEmployee(MyAppointment) == true)
            {
                // save button is clicked
                return RedirectToPage("/elderly/appointment/Retrieve");
            }
            else
                return BadRequest();
        }

    }
}
