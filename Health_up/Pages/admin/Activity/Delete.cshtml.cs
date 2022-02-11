using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Health_up.Pages.admin.Activity
{
    public class DeleteModel : PageModel
    {
        private readonly ILogger<DeleteModel> _logger;


        private readonly ActivityService _svc;
        public DeleteModel(ILogger<DeleteModel> logger, ActivityService service)
        {
            _logger = logger;

            _svc = service;
        }
        [BindProperty]
        public Models.Activity Myactivity { get; set; }
        [BindProperty]
        public string MyMessage { get; set; }
        public IActionResult OnGet(string id)
        {
            if (id != null)
            {
                Myactivity = _svc.GetActivityById(id);

            }
            Myactivity = _svc.GetActivityById(id);
            if (_svc.DeleteActivity(Myactivity))
            {
                return RedirectToPage("/admin/Activity/View");
            }
            else
            {
                MyMessage = "Appointment Id does not exist!";
                return Page();
            }
        }
    }
}
