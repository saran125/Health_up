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
            newactivity.Id = Guid.NewGuid().ToString();
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
            if (theactivity == null)
            {
                return true;
            }
            else
            {
                try
                {
                    
                    _context.Remove(theactivity);
                    _context.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActivityExists(theactivity.Id))
                    {
                        return false;
                    }
                    else
                    {
                        throw;
                    }
                }
            }

        }
        public Activity GetActivityById(String id)
        {
            Activity theActivity = _context.Activitys.Where(e => e.Id == id).FirstOrDefault();
            return theActivity;
        }
        public string GetActivityName(string Id)
        {
            Activity theActivity = _context.Activitys.Where(e => e.Id == Id).FirstOrDefault();
            return theActivity.Activity_name;
        }
        public int CurrentActivity()
        {
            List<Activity> AllActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
            int current = 0;
            foreach(var e in AllActivity)
            {
                var time = (e.Activity_start_date - DateTime.Now).Days;
                var endtime = (e.Activity_end_date - DateTime.Now).Days;
                if(time <= 0 && endtime >= 0)
                {
                    current += 1;
                }
            }
            return current;
        }
        public int UpcomingActivity()
        {
            List<Activity> AllActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
            int current = 0;
            foreach (var e in AllActivity)
            {
                var time = (e.Activity_start_date - DateTime.Now).Days;
                if (time > 0)
                {
                    current += 1;
                }

            }
            return current;
        }
        public int PastActivity()
        {
            List<Activity> AllActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
            int current = 0;
            foreach (var e in AllActivity)
            {
             
                var endtime = (e.Activity_end_date - DateTime.Now).Days;
                if (endtime <= 0)
                {
                    current += 1;
                }

            }
            return current;
        }
        public List<Activity> AllCurrentActivity()
        {
            List<Activity> AllActivity = new List<Activity>();
            List<Activity> AllCurrentActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
    
            foreach (var e in AllActivity)
            {
                var time = (e.Activity_start_date - DateTime.Now).Days;
                var endtime = (e.Activity_end_date - DateTime.Now).Days;
                if (time <= 0 && endtime >= 0)
                {
                    AllCurrentActivity.Add(e);
                }
            }
            return AllCurrentActivity;
        }
        public List<Activity> AllUpcomingActivity()
        {
            List<Activity> AllActivity = new List<Activity>();
            List<Activity> AllUpcomingActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
           
            foreach (var e in AllActivity)
            {
                var time = (e.Activity_start_date - DateTime.Now).Days;
                if (time > 0)
                {
                    AllUpcomingActivity.Add(e);
                }

            }
            return AllUpcomingActivity;
        }
        public List<Activity> AllPastActivity()
        {
            List<Activity> AllActivity = new List<Activity>();
            List<Activity> AllPastActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
   
            foreach (var e in AllActivity)
            {
                var endtime = (e.Activity_end_date - DateTime.Now).Days;
                if (endtime <= 0)
                {
                    AllPastActivity.Add(e);
                }

            }
            return AllPastActivity;
        }
        public List<Activity> AllActivity()
        {
            List<Activity> AllActivity = new List<Activity>();
            List<Activity> AllPastActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();

            foreach (var e in AllActivity)
            {
                var endtime = (e.Activity_end_date - DateTime.Now).Days;
                if (endtime > 0)
                {
                    AllPastActivity.Add(e);
                }

            }
            return AllPastActivity;
        }
        public List<Activity> Activity()
        {
            List<Activity> AllActivity = new List<Activity>();
            AllActivity = _context.Activitys.ToList();
            return AllActivity;
        }

    }
}
