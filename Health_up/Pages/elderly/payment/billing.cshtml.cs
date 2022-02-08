using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using HealthUP.Pages.payment;

namespace HealthUP.Pages.payment
{
    public class billingModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                return RedirectToPage("payment");
            }
            return Page();
        }
    }
}
