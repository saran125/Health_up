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
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;
        private readonly DoctorTimeslotService _svc;
        public DeleteModel(ILogger<DeleteModel> logger, DoctorTimeslotService service)
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
            }
            else
                return RedirectToPage("Index");

            if (_svc.DeleteTimeslot(DocTimeslot))
            {
                return RedirectToPage("/doctor/timeslot/Retrieve");
            }
            else
            {
                return Page();
            }
        }
    }
}
