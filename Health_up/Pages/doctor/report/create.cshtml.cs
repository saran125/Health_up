using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health_up.Models;
using Health_up.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Health_up.Pages.doctor.report
{
    public class medical_reportModel : PageModel
    {
        private readonly ILogger<medical_reportModel> _logger;
        private ReportService _svc;
        public medical_reportModel(ILogger<medical_reportModel> logger, ReportService service)
        {
            _logger = logger;
            _svc = service;
        }
        [BindProperty]
        public MedicalReport report { get; set; }
        public string msg { get; set; }
        public string docemail { get; set; }
        public void OnGet()
        {
            docemail = HttpContext.Session.GetString("Email");
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //make unique uuid for report

                if(_svc.AddReport(report))
                {
                    return RedirectToPage("success");

                }
                else
                {
                    msg = "report already exist";
                    return Page();
                }

            }
            return Page();
        }

    }
}
