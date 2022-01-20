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
    public class CreateModel : PageModel
    {
        private readonly ILogger<CreateModel> _logger;
        private DoctorTimeslotService _svc;
        public CreateModel(ILogger<CreateModel> logger, DoctorTimeslotService service)
        {
            _logger = logger;
            _svc = service;
        }
        [BindProperty]
        public DoctorTimeslot DocTimeslot { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddTimeslot(DocTimeslot))
                {
                    return RedirectToPage("/doctor/timeslot/Retrieve");

                }
                else
                {
                    return Page();
                }

            }
            return Page();
        }
    }
}
