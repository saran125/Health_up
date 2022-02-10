using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using crypto;
using Health_up.Models;
using Health_up.PayPalHelper;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages.elderly.appointments
{
    public class CreateModel : PageModel

    {
        private IConfiguration _configuration { get; }
        private readonly ILogger<CreateModel> _logger;
        private AppointmentService _svc;

        public CreateModel(ILogger<CreateModel> logger, AppointmentService service, IConfiguration configuration)
        {
            _logger = logger;
            _svc = service;
            _configuration = configuration;
            
        }

        [BindProperty]
        public Appointment MyAppointment { get; set; }

        [BindProperty]
        public string MyMessage { get; set; }


        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost(double consultationFee)
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddAppointment(MyAppointment))
                {
                    var payPalAPI = new PayPalAPI(_configuration);
                    string url = await payPalAPI.getRedirectURLToPayPal(consultationFee, "SGD");
                    return Redirect(url);

                }
                else
                {
                    MyMessage = "Appointment Id already exist!";
                    return Page();
                }


            }
            return Page();
        }
    }
}
