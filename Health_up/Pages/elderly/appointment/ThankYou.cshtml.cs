using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.PayPalHelper;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages.elderly.appointments
{
    public class ThankYouModel : PageModel
    {
        private IConfiguration _configuration { get; }

        private readonly ILogger<ThankYouModel> _logger;
        private AppointmentService _svc;
        public ThankYouModel(ILogger<ThankYouModel> logger, AppointmentService service, IConfiguration configuration)
        {
            _logger = logger;
            _svc = service;
            _configuration = configuration;

        }
        [BindProperty]
        public Appointment MyAppointment { get; set; }

        [BindProperty]
        public string MyMessage { get; set; }



        public async Task<IActionResult> OnGetAsync([FromQuery(Name ="paymentId")] string paymentId, [FromQuery(Name = "token")] string token, [FromQuery(Name = "PayerID")] string payerID)
        {
            var payPalAPI = new PayPalAPI(_configuration);
            PayPalPaymentExecutedResponse result = await payPalAPI.executedPayment(paymentId, payerID);
            Debug.WriteLine("Transaction Details");
            Debug.WriteLine("cart : " + result.cart);
            Debug.WriteLine("create time : "+ result.create_time.ToLongDateString());
            Debug.WriteLine("id : "+result.id);
            Debug.WriteLine("intent : "+result.intent);
            Debug.WriteLine("links 0 - href :" + result.links[0].href);
            Debug.WriteLine("links 0 - method :" + result.links[0].method);
            Debug.WriteLine("links 0 - rel :" + result.links[0].rel);
            Debug.WriteLine("payer_info - first name :" + result.payer.payer_info.first_name);
            Debug.WriteLine("payer_info - last name :" + result.payer.payer_info.last_name);
            Debug.WriteLine("payer_info - email :" + result.payer.payer_info.email);
            Debug.WriteLine("payer_info - billing_address :" + result.payer.payer_info.billing_address);
            Debug.WriteLine("payer_info - country_code :" + result.payer.payer_info.country_code);
            Debug.WriteLine("payer_info - shipping_address :" + result.payer.payer_info.shipping_address);
            Debug.WriteLine("payer_info - payer_id :" + result.payer.payer_info.payer_id);
            Debug.WriteLine("state :" + result.state);
            return Page();

        }
        //public IActionResult OnPost()
        //{


        //}

    }
}
