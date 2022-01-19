﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using crypto;
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

        private static string CreateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }
        public bool AddUser(User newuser)
        {
            if (UserExists(newuser.NRIC))
            {
                return false;
            }
            else
            {
                var password = Security.ComputeHash(newuser.Password, CreateSalt(8));
                newuser.Password = password;
                byte[] cipherKey;
                byte[] cipherIV;
                var nric = Security.Encrypt(newuser.NRIC, cipherKey, cipherIV);

                _context.Add(newuser);
                _context.SaveChanges();
                return true;
            }
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.NRIC == id);
        }

    }
}
