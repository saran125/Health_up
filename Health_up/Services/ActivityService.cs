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
            return _context.Activitys.Any(e => e.Id == id);
        }
        public List<Activity> GetAllActivitys()
        {
            List<Activity> AllActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
            return AllActivity;
        }
        public bool UpdateActivity(Activity theactivity)
        {
            bool updated = true;
            _context.Attach(theactivity).State = EntityState.Modified;
            try
            {
                _context.Update(theactivity);
                _context.SaveChanges();
                updated = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(theactivity.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;
        }
        public bool DeleteActivity(Activity theactivity)
        {
            bool deleted = true;
            try
            {
                _context.Remove(theactivity);
                _context.SaveChanges();
                deleted = true;


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(theactivity.Id))
                {
                    deleted = false;
                }
                else
                {
                    throw;
                }
            }
            return deleted;

        }
        public Activity GetActivityById(String id)
        {
            Activity theActivity = _context.Activitys.Where(e => e.Id == id).FirstOrDefault();
            return theActivity;
        }
    }
}
