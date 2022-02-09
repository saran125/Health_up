using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.Auth
{


   
    public class VerifyEmailModel : PageModel
    {

        private readonly ILogger<VerifyEmailModel> _logger;
        private UserService _svc;
        public VerifyEmailModel(ILogger<VerifyEmailModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }
        [BindProperty]
        public User MyUser { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        [BindProperty]
        public string Email { get; set; }
        public IActionResult OnGet()
        {
            if (Request.Cookies["Email"] == null)
            {
                return Redirect("/forbidden");
            }
            else
            {
                Email = Request.Cookies["Email"];
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.Verify(MyUser))
                {
                    return Redirect("/Auth/EmailVerified");
                }
                else
                {
                    MyMessage = "Invalid Code!";
                    return Page();
                }
               
            }
            return Page();
        }
    }
}
