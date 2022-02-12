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

namespace Health_up.Pages.elderly
{
    public class EditProfileModel : PageModel
    {
        private readonly ILogger<EditProfileModel> _logger;
        private UserService _svc;

        public EditProfileModel(ILogger<EditProfileModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;

        }

        [BindProperty]
        public User MyUser { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") != "elderly")
            {
                return Redirect("/forbidden");
            }
            MyUser = _svc.GetUserById(HttpContext.Session.GetString("Email"));
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.UpdateUser(MyUser))
                {
                    return Redirect("/elderly/Profile");
                }
            }
           
                return Page();
            
        }
    }
}
