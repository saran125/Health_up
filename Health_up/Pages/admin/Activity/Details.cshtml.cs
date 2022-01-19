using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Health_up.Pages.admin.Activity
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Models.Activity MyActivity { get; set; }


        private ActivityService _svc;

        public DetailsModel(ActivityService service)
        {
            _svc = service;

        }

        public void OnGet(string id)
        {
            if (id != null)
            {
                int a = 1;
                MyActivity = _svc.GetActivityById(id);
                a = a + 1;
            }

        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/admin/Activity/View");
        }
    }
}
