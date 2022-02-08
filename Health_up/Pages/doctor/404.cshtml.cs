using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Health_up.Pages.doctor
{
    public class _404Model : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("/doctor/index");
        }
    }
}
