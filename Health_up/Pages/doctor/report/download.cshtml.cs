using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.doctor.report
{
    public class downloadModel : PageModel
    {
        private readonly ILogger<downloadModel> _logger;
        private ReportService _svc;
        public downloadModel(ILogger<downloadModel> logger, ReportService service)
        {
            _logger = logger;
            _svc = service;
        }
        public string msg { get; set; }

        [BindProperty]
        public MedicalReport report { get; set; }
        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                report = _svc.GetReportById(id);
                return Page();
            }
            else return RedirectToPage("/doctor/404");
        }
    }
}
