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


namespace HealthUP.Pages.elderly.booking
{
    public class CreateModel : PageModel

    {

        private readonly ILogger<CreateModel> _logger;
        private IConfiguration _configuration { get; }
        private BookingService _svc;
        private ActivityService _actsv;

        
        public CreateModel(ILogger<CreateModel> logger, BookingService service, ActivityService service1, IConfiguration configuration)
        {
            _logger = logger;
            _svc = service;
            _configuration = configuration;
            _actsv = service1;
        }

        [BindProperty]
        public Booking MyBooking { get; set; }
        public Activity MyActivity { get; set; }

        [BindProperty]
        public string MyMessage { get; set; }
        public string id = Security.GenerateText(16);

        public void OnGet(string id)
        {
           MyActivity = _actsv.GetActivityById(id);

        }
       

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                MyBooking.elderly_id = HttpContext.Session.GetString("Email");


                if (_svc.AddBooking(MyBooking))
                {
                    var payPalAPI = new PayPalAPI(_configuration);
                    string url = await payPalAPI.getRedirectURLToPayPal(MyBooking.ActivityPrice,"SGD");
                    return Redirect(url);

                }
                else
                {
                    MyMessage = " Id already exist!";
                    return Page();
                }


            }
            return Page();
        }
    }
}
