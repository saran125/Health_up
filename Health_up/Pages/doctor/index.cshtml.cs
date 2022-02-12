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
        private readonly ILogger<indexModel> _logger;
        private AppointmentService _svc;
        private UserService _usvc;
        public indexModel(ILogger<indexModel> logger, AppointmentService service, UserService userService)
        {
            _logger = logger;
            _svc = service;
            _usvc = userService;
        }
        [BindProperty]
        public List<Appointment> appointmentByEmail { get; set; }
        public List<User> userInfo { get; set; }

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
            appointmentByEmail = _svc.GetAppointmentByDocEmail(HttpContext.Session.GetString("Email"));
            userInfo = _usvc.GetAllUsers();

        }
        public IActionResult OnPost()
        {
            return RedirectToPage("report/create");
        }
    }
}
