using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Health_up.Pages.admin.Activity
{
    public class DshboardModel : PageModel
    {
        public IActionResult OnGet(string Id)
        {
            return Page();
        }
    }
}
