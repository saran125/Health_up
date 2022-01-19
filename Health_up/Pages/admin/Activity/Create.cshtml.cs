using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health_up.Models;
using Health_up.Services;
using crypto;

namespace Health_up.Pages.admin
{
    public class ActivityModel : PageModel
    {
        [BindProperty]
        public Models.Activity Myactivity { get; set; }
        public string id = Security.GenerateText(16);
        
        private readonly ActivityService _svc;
        public ActivityModel(ActivityService service)
        {
            _svc = service;

        }
      
        [BindProperty]
        public string MyMessage { get; set; }

        public void OnGet()
        {
           
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (_svc.AddActivity(Myactivity))
                {
                    return RedirectToPage("/admin/Activity/View");

                }

                else
                {
                    return RedirectToPage("/admin/Activity/Create");
                }
            }
            return Page();

        }
    }
}
