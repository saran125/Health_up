using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health_up.Models;

namespace Health_up.Pages.doctor
{
    public class medical_reportModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // Create session
                return RedirectToPage("success");

            }
            return Page();
        }

    }
}
