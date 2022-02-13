using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthUP.Pages.payment;
using Microsoft.Extensions.Logging;
using Health_up.Services;
using Health_up.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Health_up.PayPalHelper;

namespace HealthUP.Pages.payment
{
    public class billingModel : PageModel
    {
        private IConfiguration _configuration { get; }
        private readonly ILogger<billingModel> _logger;
        private ReportService _svc;
        public billingModel(ILogger<billingModel> logger, ReportService service, IConfiguration configuration)
        {
            _logger = logger;
            _svc = service;
            _configuration = configuration;
        }
        [BindProperty]
        public MedicalReport report { get; set; }
        public void OnGet()
        {
           
            string email = HttpContext.Session.GetString("Email");
            List<MedicalReport> docappts = _svc.GetAllReports().Where(e => e.Doctor_ID == email).ToList();
        }
        public IActionResult OnPost()
        {
            
                return RedirectToPage("payment");
            
        }
        /*
        public async Task<IActionResult> OnPost(double medicineFee)
        {
            System.Diagnostics.Debug.WriteLine("This err");
            System.Diagnostics.Debug.WriteLine(report.Medicine_Cost);
            System.Diagnostics.Debug.WriteLine(medicineFee);


            if (ModelState.IsValid)
            {
                var payPalAPI = new PayPalAPI(_configuration);
                string url = await payPalAPI.getRedirectURLToPayPal(medicineFee, "SGD");
                return Redirect(url);

            }
            return Page();
        }
        */

    }
}
