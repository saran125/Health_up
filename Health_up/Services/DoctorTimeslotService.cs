using Health_up.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health_up.Services
{
    public class DoctorTimeslotService
    {
        private Models.HealthUPDbContext _context;
        public DoctorTimeslotService(Models.HealthUPDbContext context)
        {
            _context = context;
        }

        public bool AddTimeslot(DoctorTimeslot newtimeslot)
        {
            if (DoctorTimeslotExists(newtimeslot.Id))
            {
                return false;
            }
            _context.Add(newtimeslot);
            _context.SaveChanges();
            return true;
        }
        public List<DoctorTimeslot> GetAllTimeslots()
        {
            List<DoctorTimeslot> AllTimelots = new List<DoctorTimeslot>();
            AllTimelots = _context.DoctorTimeslots.ToList();
            return AllTimelots;
        }

        private bool DoctorTimeslotExists(string id)
        {
            return _context.DoctorTimeslots.Any(e => e.Id == id);
        }

        public DoctorTimeslot GetTimeslotById(String id)
        {
            DoctorTimeslot theTimeslotID = _context.DoctorTimeslots.Where(e => e.Id == id).FirstOrDefault();
            return theTimeslotID;
        }

        public bool UpdateTimeslot(DoctorTimeslot theTimeslotID)
        {
            bool updated = true;
            _context.Attach(theTimeslotID).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorTimeslotExists(theTimeslotID.Id))
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
        public bool DeleteTimeslot(DoctorTimeslot theTimeslotID)
        {
            bool deleted = true;
            try
            {
                _context.Remove(theTimeslotID);
                _context.SaveChanges();
                deleted = true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorTimeslotExists(theTimeslotID.Id))
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

        public List<DoctorTimeslot> GetTimeslotsByEmail(String email)
        {
            List<DoctorTimeslot> TimeslotsByEmail = new List<DoctorTimeslot>();
            TimeslotsByEmail = _context.DoctorTimeslots.Where(e => e.DoctorId == email).ToList();
            return TimeslotsByEmail;
        }
    }
}
