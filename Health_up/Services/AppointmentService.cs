using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_up.Services
{
    public class AppointmentService
    {
        private Models.HealthUPDbContext _context;
        public AppointmentService(Models.HealthUPDbContext context)
        {
            _context = context;
        }


        public bool AddAppointment(Appointment newappointment)
        {
            newappointment.Id = Guid.NewGuid().ToString();
            if (AppointmentExists(newappointment.Id))
            {
                return false;
            }
            _context.Add(newappointment);
            _context.SaveChanges();
            return true;
        }
        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> AllAppointments = new List<Appointment>();
            AllAppointments = _context.Appointments.ToList();
            return AllAppointments;
        }

        public Appointment GetAppointmentById(String id)
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
            Appointment theAppointment = _context.Appointments.Where(e => e.Id == id).FirstOrDefault();
            return theAppointment;
        }

        private bool AppointmentExists(string id)
        {
            return _context.Appointments.Any(e => e.Id == id);
        }
        public bool UpdateAppointment(Appointment theappointment)
        {
            bool updated = true;
            _context.Attach(theappointment).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(theappointment.Id))
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
        public bool DeleteAppointment(Appointment theappointment)
        {
            bool deleted = true;
            try
            {
                _context.Remove(theappointment);
                _context.SaveChanges();
                deleted = true;


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppointmentExists(theappointment.Id))
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
