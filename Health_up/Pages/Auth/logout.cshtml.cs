using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Health_up.Pages.Auth
{
    public class logoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            
                HttpContext.Session.Clear();
                Response.Cookies.Delete("Email");
                return RedirectToPage("/Index");
            
           
        }
    }
}
