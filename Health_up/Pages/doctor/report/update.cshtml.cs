
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health_up.Services;
using Health_up.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

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
        [BindProperty]
        public string docemail { get; set; }
        public string setid { get; set; }
        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                docemail = HttpContext.Session.GetString("Email");
                report = _svc.GetReportById(id);
                setid = id;
                System.Diagnostics.Debug.WriteLine(setid);
                report.Report_id = setid;
                System.Diagnostics.Debug.WriteLine(report.Report_id);

                return Page();
            }
            else return RedirectToPage("/doctor/404");
        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            System.Diagnostics.Debug.WriteLine("ID HERE");
            System.Diagnostics.Debug.WriteLine(setid);
            System.Diagnostics.Debug.WriteLine(report.Report_id);
            if (_svc.UpdateReport(report) == true)
            {
                return RedirectToPage("/doctor/report/view");
            }
            else msg = "report already exist"; return Page();
            
        }
    }
}
