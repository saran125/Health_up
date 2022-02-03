using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health_up.Services;
using Health_up.Models;
using Microsoft.Extensions.Logging;
using System.IO;

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
        [BindProperty]
        public List<String> datelist { get; set; }
        [BindProperty]
        public string errormsg { get; set; }
        public List<MedicalReport> sort(List<MedicalReport> allreport)
        {
            return allreport.OrderBy(x => DateTime.Parse(x.Report_date.ToString())).ToList();
        }
        public List<String> unique(List<String> strin)
        {
            return strin.Distinct().ToList();
        }
        public void OnGet()
        {

            List<MedicalReport> allreport = _svc.GetAllReports();
            List<MedicalReport> temp = sort(allreport);
            List<String> strin = new List<string>();
            foreach (var i in allreport)
            {
                if(strin.Contains(i.Report_date.ToString().Split(" ")[0]) == false)
                {
                    strin.Add(i.Report_date.ToString().Split(" ")[0]);
                }
            }

            reports = temp;
            datelist = unique(strin);

        }
        public IActionResult OnPost(string query)
        {
            if(query == null)
            {
                List<MedicalReport> allreport = _svc.GetAllReports();
                List<MedicalReport> temp = sort(allreport);
                List<String> strin = new List<string>();
                foreach (var i in allreport)
                {
                    if (strin.Contains(i.Report_date.ToString().Split(" ")[0]) == false)
                    {
                        strin.Add(i.Report_date.ToString().Split(" ")[0]);
                    }
                }

                reports = temp;
                datelist = unique(strin);
                return Page();
            }
            else
            {
                if(DateTime.TryParse(query,out DateTime date))
                {
                    List<MedicalReport> allreport = _svc.GetAllReports();
                    List<MedicalReport> temp = new List<MedicalReport>();
                    List<String> strin = new List<string>();
                    foreach (var i in allreport)
                    {                       
                            if(i.Report_date.ToString().Split(" ")[0] == query)
                        {
                                temp.Add(i);
                            strin.Add(i.Report_date.ToString().Split(" ")[0]);

                        }
                        }
                    

                    reports = temp;
                    datelist = unique(strin);
                    return Page();
                }
                else
                {
                    errormsg = "Input not of type DD/MM/YYYY";
                    List<MedicalReport> allreport = _svc.GetAllReports();
                    List<MedicalReport> temp = sort(allreport);
                    List<String> strin = new List<string>();
                    foreach (var i in allreport)
                    {
                        if (strin.Contains(i.Report_date.ToString().Split(" ")[0]) == false)
                        {
                            strin.Add(i.Report_date.ToString().Split(" ")[0]);
                        }
                    }

                    reports = temp;
                    datelist = unique(strin);
                    return Page();
                }
            }
        }
    }
}
