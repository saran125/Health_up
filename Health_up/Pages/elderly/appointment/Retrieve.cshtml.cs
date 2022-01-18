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
    public class RetrieveModel : PageModel
    {
        [BindProperty]
        public List<Appointment> allappointments { get; set; }

        private readonly ILogger<RetrieveModel> _logger;


        private readonly AppointmentService _svc;
        public RetrieveModel(ILogger<RetrieveModel> logger, AppointmentService service)
        {
            _logger = logger;

            _svc = service;
        }

        public void OnGet()
        {
            allappointments = _svc.GetAllAppointments();

        }
        //public IActionResult OnPost()
        //{
        //}
    }
}
