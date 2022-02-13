using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Health_up.Models;
using Health_up.Services;
using crypto;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Health_up.Pages.admin
{
    public class ActivityModel : PageModel
    {
        [BindProperty]
        public Models.Activity Myactivity { get; set; }
        private readonly ActivityService _svc;
        public ActivityModel(ActivityService service)
        {
            _svc = service;
        }
      
        [BindProperty]
        public string MyMessage { get; set; }
        [BindProperty]
        public byte[] Image { get; set; }

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
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                foreach (var file in Request.Form.Files)
                {
                    MemoryStream ms = new MemoryStream();
                    file.CopyTo(ms);
                    Image = ms.ToArray();
                    Myactivity.Activity_photo = Convert.ToBase64String(Image);
                    ms.Close();
                    ms.Dispose();
                }
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
