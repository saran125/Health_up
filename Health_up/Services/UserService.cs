using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_up.Services
{
    public class UserService
    {
            
        private HealthUPDbContext _context;
        public UserService(HealthUPDbContext context)
        {
            _context = context;
        }


        public bool AddUser(User newuser)
        {
            if (UserExists(newuser.NRIC))
            {
                return false;
            }
            _context.Add(newuser);
            _context.SaveChanges();
            return true;
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.NRIC == id);
        }

    }
}
