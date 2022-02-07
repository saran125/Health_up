using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private UserService _svc;
        public RegisterModel(ILogger<RegisterModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public User MyUser { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        [BindProperty]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
           ErrorMessage = "Password Need to be at least 8 characters, combination of lower case, upper case, numbers & special characters")]
        public string Cfmpwd { get; set; }

        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Cfmpwd == MyUser.Password)
                {

                    if (_svc.Register(MyUser))
                    {
                        return RedirectToPage("/Auth/login");
                    }
                    else
                    {
                        MyMessage = "User Id already exist!";
                        return Page();
                    }
                }
                else
                {
                    MyMessage = "The Password is not similar!";
                    return Page();
                }
            }
            return Page();
        }

    }
}
