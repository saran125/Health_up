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
    public class EditModel : PageModel
    {
        private readonly ILogger<EditModel> _logger;
        private DoctorTimeslotService _svc;
        public EditModel(ILogger<EditModel> logger, DoctorTimeslotService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public DoctorTimeslot DocTimeslot { get; set; }

        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                DocTimeslot = _svc.GetTimeslotById(id);

                return Page();
            }
            else
                return RedirectToPage("/doctor/404");

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (_svc.UpdateTimeslot(DocTimeslot) == true)
            {
                // save button is clicked
                return RedirectToPage("/doctor/timeslot/Retrieve");
            }
            else
                return BadRequest();
        }
    }
}
