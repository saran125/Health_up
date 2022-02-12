using System;
using System.Collections.Generic;
using HealthUP;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace HealthUP.Pages.admin
{
    public class usersModel : PageModel
    {
        private readonly ILogger<usersModel> _logger;

        private ActivityService _svc;
        private UserService _Usersvc;
        public usersModel(ILogger<usersModel> logger, ActivityService service, UserService service1)
        {
            _logger = logger;
            _svc = service;
            _Usersvc = service1;
        }
        public int Users { get; set; }
        public int Doctor { get; set; }
        public int Elderly { get; set; }
        public List<Health_up.Models.User> alluser { get; set; }

        public IActionResult OnGet()
        {
            if ((HttpContext.Session.GetString("Role") != "admin"))
            {
                return Redirect("/forbidden");
            }
                Users = _Usersvc.Users();
            Doctor = _Usersvc.GetAllDoctors().Count;
            Elderly = _Usersvc.GetTheElderly().Count;
            alluser = _Usersvc.GetAllUsers();
        
            return Page();
        }
    }
}
