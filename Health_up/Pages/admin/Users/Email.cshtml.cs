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

namespace Health_up.Pages.admin.Users
{
    public class EmailModel : PageModel
    {
        private readonly ILogger<EmailModel> _logger;


        private UserService _Usersvc;
        public EmailModel(ILogger<EmailModel> logger, ActivityService service, UserService service1)
        {
            _logger = logger;
            _Usersvc = service1;
        }
        [BindProperty]
        public User theuser {get;set;}
        [BindProperty]
        public string subject { get; set; }
        [BindProperty]
        public string message { get; set; }
        [BindProperty]
        public string email { get; set; }
        public IActionResult OnGet(string Id)
        {
            if (HttpContext.Session.GetString("Role") == "admin")
            {
                theuser = _Usersvc.GetUserById(Id);
                return Page();
            }
            else
            {
                return Redirect("/forbidden");
            }
        }
    }
}
