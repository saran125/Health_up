using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthUP.Pages.doctor.report
{
    public class success_reportModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {
            if(ModelState.IsValid)
            {
                 Response.Redirect("../index");

            }
             Page();
        }
    }
}
