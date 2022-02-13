using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_up.Services
{
    public class ActivityFeedbackService
    {
        private HealthUPDbContext _context;
        public ActivityFeedbackService(Models.HealthUPDbContext context)
        {
            _context = context;
        }

        public bool AddFeedback(Activity_Feedback newfeedback)
        {
            newfeedback.Id = Guid.NewGuid().ToString();
            newfeedback.Date = DateTime.Now;
            _context.Add(newfeedback);
            _context.SaveChanges();
            return true;
        }
        public bool FeedbackExists(string id)
        {
            return _context.Activity_Feedback.Any(e => e.Id == id);
        }
        public List<Activity_Feedback> GetAllFeedbacks()
        {
            List<Activity_Feedback> AllActivity = new List<Activity_Feedback>();
            AllActivity = _context.Activity_Feedback.ToList();
            return AllActivity;
        }
      
        public bool DeleteFeedback(Activity_Feedback thefeedback)
        {
            if (thefeedback == null)
            {
                return true;
            }
            else
            {
                try
                {
                    _context.Remove(thefeedback);
                    _context.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(thefeedback.Id))
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
        public bool ReplyFeedback(Activity_Feedback thefeedback)
        {
            if (thefeedback == null)
            {
                return true;
            }
            else
            {
                try
                {
                    Activity_Feedback Replyfeedback = FeedbackbyId(thefeedback.Id);
                    Replyfeedback.Reply = thefeedback.Reply;
                    _context.Attach(Replyfeedback).State = EntityState.Modified;
                    _context.Update(Replyfeedback);
               
                    _context.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedbackExists(thefeedback.Id))
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
       
        public Activity_Feedback FeedbackbyId(string Id)
        {

            Activity_Feedback AllFeedback = _context.Activity_Feedback.Where(e => e.Id == Id).FirstOrDefault();
            return AllFeedback;
        }
        public List<Activity_Feedback> FeedbackbyActivityId(string Id)
        {
            List<Activity_Feedback> AllFeedback = new List<Activity_Feedback>();
            AllFeedback = _context.Activity_Feedback.Where(e => e.activity_id == Id).ToList();
            return AllFeedback;
        }

       
    }
}
