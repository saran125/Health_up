using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using crypto;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

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
        public bool Register(User newuser)
        {
            if (UserExists(newuser.Email))
            {
                return false;
            }
            else
            {
                newuser.Verify = "False";
                var password = BC.HashPassword(newuser.Password);
                newuser.Password = password;
                
                /*byte[] cipherKey;
                byte[] cipherIV;
                var nric = Security.Encrypt(newuser.NRIC, cipherKey, cipherIV);

               var nric = Security.Encrypt(newuser.NRIC, cipherKey, iv);
                newuser.NRIC = nric;
                */
                _context.Add(newuser);
                _context.SaveChanges();
                return true;
            }
        }

        public bool Login(User existuser)
        {
            
            try
            {
                if (!UserExists(existuser.Email))
                {
                    return false;
                }

                else {
                  //  return true;
                   
                    User account = GetUserById(existuser.Email);
                    if (BC.Verify(existuser.Password, account.Password))
                    {

                        return true;

                    }
                    else

                        // authentication successful
                        return false;
                    

                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
            
            
        }
        public User Theuser(User existuser)
        {
            User account = GetUserById(existuser.Email);
            return account;
        }
        public User GetUserById(string Email)
        {
            User theuser = _context.Users.SingleOrDefault(o => o.Email == Email);
            return theuser;
        }
        private bool UserExists(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }


    }
}
