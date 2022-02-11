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

namespace Health_up.Pages.elderly.appointment
{
    public class HistoryModel : PageModel
    {
        [BindProperty]
        public List<Appointment> appointmentsByEmail { get; set; }

        private readonly ILogger<HistoryModel> _logger;


        private readonly AppointmentService _svc;
        public HistoryModel(ILogger<HistoryModel> logger, AppointmentService service)
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
            appointmentsByEmail = _svc.GetAppointmentByEmail(HttpContext.Session.GetString("Email"));

        }
    }
}
