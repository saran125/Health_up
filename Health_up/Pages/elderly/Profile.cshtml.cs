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
    public class ProfileModel : PageModel
    {
        [BindProperty]
      
        public User theuser { get; set; }
        private readonly ILogger<ProfileModel> _logger;

        private UserService _svc;
        public ProfileModel(ILogger<ProfileModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }
        public string ID { get; set; }
        public Models.Activity Myactivity { get; set; }
        public IActionResult OnGet()
        {
            theuser = _svc.GetUserById(@HttpContext.Session.GetString("Email"));
            return Page();
        }
    }
}
