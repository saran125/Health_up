using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health_up.Services;
using Health_up.Models;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.doctor.report
{
    public class updateModel : PageModel
    {
        private readonly ILogger<updateModel> _logger;
        private ReportService _svc;
        public updateModel(ILogger<updateModel> logger, ReportService service)
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
            else return RedirectToPage("index");
        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            if (_svc.UpdateReport(report) == true)
            {
                return RedirectToPage("/doctor/report/view");
            }
            else msg = "report already exist"; return Page();
            
        }
    }
}
