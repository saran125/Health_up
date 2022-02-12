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
    public class ChangePasswordModel : PageModel
    {
        private readonly ILogger<ChangePasswordModel> _logger;
        private UserService _svc;
        public ChangePasswordModel(ILogger<ChangePasswordModel> logger, UserService service)
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
    }
}
