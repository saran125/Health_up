using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.doctor.timeslot
{
    public class RetrieveModel : PageModel
    {
        [BindProperty]
        public List<DoctorTimeslot> alltimeslots { get; set; }

        private readonly ILogger<RetrieveModel> _logger;


        private readonly DoctorTimeslotService _svc;
        public RetrieveModel(ILogger<RetrieveModel> logger, DoctorTimeslotService service)
        {
            _logger = logger;

            _svc = service;
        }

        public void OnGet()
        {
            alltimeslots = _svc.GetAllTimeslots();

        }
    }
}
