using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Health_up.Services;
using Health_up.Models;

namespace Health_up.Pages.doctor.report
{
    public class deleteModel : PageModel
    {
        private readonly ILogger<deleteModel> _logger;
        private readonly ReportService _svc;
        public deleteModel(ILogger<deleteModel> logger, ReportService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public MedicalReport report { get; set; }
        public IActionResult OnGet(string id)
        {
            if(id != null)
            {
                report = _svc.GetReportById(id);
            }
            else { return RedirectToPage("Index"); 
            }
            if(_svc.DeleteReport(report))
            {
                return RedirectToPage("/doctor/report/view");
            }
            else
            {
                return Page();
            }
        }
    }
}
