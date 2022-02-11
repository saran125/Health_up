using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_up.Services
{
    public class BookingService
    {
        private Models.HealthUPDbContext _context;
        public BookingService(Models.HealthUPDbContext context)
        {
            _context = context;
        }


        public bool AddBooking(Booking newbooking)
        {
            if (BookingExists(newbooking.Id))
            {
                return false;
            }
            _context.Add(newbooking);
            _context.SaveChanges();
            return true;
        }
        public List<Booking> GetAllBookings()
        {
            List<Booking> AllBookings = new List<Booking>();
            AllBookings = _context.Bookings.ToList();
            return AllBookings;
        }
        public List<Booking> GetByActivityBookings(string Id)
        {
            List<Booking> AllBookings = new List<Booking>();
            AllBookings = _context.Bookings.Where(e => e.Id == Id).ToList();
            return AllBookings;
        }

        public Booking GetBookingById(String id)
        {
            //List<Employee> AllEmployees = new List<Employee>();

            //Employee employee = null;
            //foreach (Employee item in AllEmployees)
            //{
            //    if (item.Id == id)
            //    {
            //        employee = item;
            //    }
            //}
            Booking theBooking = _context.Bookings.Where(e => e.Id == id).FirstOrDefault();
            return theBooking;
        }

        private bool BookingExists(string id)
        {
            return _context.Bookings.Any(e => e.Id == id);
        }


        public bool UpdateBooking(Booking thebooking)
        {
            bool updated = true;
            _context.Attach(thebooking).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(thebooking.Id))
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
        public bool DeleteBooking(Booking thebooking)
        {
            bool deleted = true;
            try
            {
                _context.Remove(thebooking);
                _context.SaveChanges();
                deleted = true;


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(thebooking.Id))
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
    }
}
