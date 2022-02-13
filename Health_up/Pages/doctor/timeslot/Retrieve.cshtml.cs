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

namespace Health_up.Pages.doctor.timeslot
{
    public class RetrieveModel : PageModel
    {
        [BindProperty]
        public List<DoctorTimeslot> timeslotsByEmail { get; set; }
        public List<Appointment> appointmentByEmail { get; set; }

        private readonly ILogger<RetrieveModel> _logger;


        private readonly DoctorTimeslotService _svc;
        private readonly AppointmentService _asvc;
        public RetrieveModel(ILogger<RetrieveModel> logger, DoctorTimeslotService service, AppointmentService appointmentService)
        {
            _logger = logger;

            _svc = service;

            _asvc = appointmentService;
        }

        public void OnGet()
        {
            timeslotsByEmail = _svc.GetTimeslotsByEmail(HttpContext.Session.GetString("Email"));
            appointmentByEmail = _asvc.GetAppointmentByDocEmail(HttpContext.Session.GetString("Email"));
        }
    }
}
