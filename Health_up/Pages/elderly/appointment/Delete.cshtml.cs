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
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;


        private readonly AppointmentService _svc;
        public DeleteModel(ILogger<DeleteModel> logger, AppointmentService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public Appointment MyAppointment { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                MyAppointment = _svc.GetAppointmentById(id);

            }
            else
                return RedirectToPage("Index");

            if (_svc.DeleteAppointment(MyAppointment))
            {
                return RedirectToPage("/elderly/appointment/Retrieve");

            }
            else
            {
                MyMessage = "Appointment Id does not exist!";
                return Page();
            }
        }
    }
}
