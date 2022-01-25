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
    public class viewModel : PageModel
    {
        private readonly ILogger<viewModel> _logger;
        private ReportService _svc;
        public viewModel(ILogger<viewModel> logger, ReportService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public List<MedicalReport> reports { get; set; }
        public int counter { get; set; }

        public void OnGet()
        {
            reports = _svc.GetAllReports();
            counter = reports.Count();
        }
    }
}
