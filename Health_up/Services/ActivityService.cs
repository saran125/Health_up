using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_up.Services
{
    public class ActivityService
    {
        private Models.HealthUPDbContext _context;
        public ActivityService(Models.HealthUPDbContext context)
        {
            _context = context;
        }

        public bool AddActivity(Activity newactivity)
        {
            if (ActivityExists(newactivity.Id))
            {
                return false;
            }
            _context.Add(newactivity);
            _context.SaveChanges();
            return true;
        }
        private bool ActivityExists(string id)
        {
            return _context.Activity.Any(e => e.Id == id);
        }


    }
}
