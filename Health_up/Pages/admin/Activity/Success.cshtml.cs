using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Health_up.Pages.admin.Activity
{
    public class SuccessModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") == "admin")
            {
                return Page();
            }
            else
            {
                return Redirect("/forbidden");
            }
        }
    }
}
