using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace HealthUP.Pages
{
    public class loginModel : PageModel
    {
        private readonly ILogger<loginModel> _logger;
        private UserService _svc;
        public loginModel(ILogger<loginModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public User MyUser { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.Login(MyUser))
                {
                    User currentuser = _svc.Theuser(MyUser);
                    HttpContext.Session.SetString("Email", currentuser.Email);
                    HttpContext.Session.SetString("Fname", currentuser.Fname);
                    HttpContext.Session.SetString("Lname", currentuser.Lname);
                    HttpContext.Session.SetString("Role", currentuser.Role);
                    
                    return RedirectToPage("/elderly/home");
                }
                else
                {
                    MyMessage = "Invalid Email or Password";
                    return Page();
                }
            }
            return Page();
        }
    }
}
