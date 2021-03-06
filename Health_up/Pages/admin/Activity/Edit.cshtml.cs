using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Health_up.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Health_up.Pages.admin
{
    public class update_activityModel : PageModel
    {
        private readonly ActivityService _svc;
        public update_activityModel(ActivityService service)
        {
            _svc = service;
        }
        [BindProperty]
        public Models.Activity Myactivity { get; set; }
        [BindProperty]
        public byte[] Image { get; set; }
        public IActionResult OnGet(string id)
        {
            if (HttpContext.Session.GetString("Role") != "admin")
            {
                return Redirect("/forbidden");
            }
            if (!_svc.ActivityExists(id))
            {
                return Redirect("/BadRequest");
            }
            if (id == null)
            {
                return Redirect("/BadRequest");
            }
            Myactivity = _svc.GetActivityById(id);
            if (Myactivity == null)
            {
                return NotFound();
            }
            return Page();
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

                if (_svc.UpdateActivity(Myactivity) == true)
                {
                    return RedirectToPage("/admin/Activity/View");
                }
                else
                {
                    return BadRequest();
                }
            }
            return Page();
        
    }
    }
}
