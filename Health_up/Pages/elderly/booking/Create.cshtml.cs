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
        [BindProperty]
        public string MyMessage { get; set; }
        [BindProperty]
        public Activity activitydetails { get; set; }
        public List<DateTime> Dates { get; set; }

        

        public IActionResult OnGet(string Id)
        {
            if (HttpContext.Session.GetString("Role") == "elderly")
            {
                activitydetails = _actsv.GetActivityById(Id);
                var dates = new List<DateTime>();
                var time = (activitydetails.Activity_start_date - DateTime.Now).Days;
                if (time < 0)
                {

                    for (var dt = DateTime.Now; dt <= activitydetails.Activity_end_date; dt = dt.AddDays(1))
                    {
                        dates.Add(dt);
                    }
                }
                else
                {
                    for (var dt = activitydetails.Activity_start_date; dt <= activitydetails.Activity_end_date; dt = dt.AddDays(1))
                    {
                        dates.Add(dt);
                    }
                }
                Dates = dates;
                return Page();
            }
            else
            {
                return Redirect("/forbidden");
            }

        }


            public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                MyBooking.elderly_id = HttpContext.Session.GetString("Email");
                if (_svc.AddBooking(MyBooking))
                {
                     var payPalAPI = new PayPalAPI(_configuration);
                     string url = await payPalAPI.getRedirectURLToPayPal(MyBooking.ActivityPrice, "SGD");
                     return Redirect(url);
                    
      

                }
                else
                {
                    MyMessage = " Id already exist!";
                    return Page();
                }


            }
            activitydetails = _actsv.GetActivityById(MyBooking.activity_id);
            return Page();
        }
    }
}
