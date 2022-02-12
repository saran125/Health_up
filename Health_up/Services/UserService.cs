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
            newuser.Email.ToLower();
            if (UserExists(newuser.Email))
            {
                return false;
            }
            else
            {
                newuser.Verify = false;
                string salt = BC.GenerateSalt(12);
                var password = BC.HashPassword(newuser.Password, salt);
                newuser.Password = password;
                if (newuser.Gender == "Male") {
                    if(newuser.Role == "elderly")
                    {
                        newuser.Photo = "Grandpa.png";
                    }
                    else if (newuser.Role == "doctor")
                    {
                        newuser.Photo = "placeholder-doctor-m-320x320.jpg";
                    }
                }
                if (newuser.Gender == "Female")
                {
                    if (newuser.Role == "elderly")
                    {
                        newuser.Photo = "307-3078505_grandma-clip-art-at-vector-clip-art-hd.png";
                    }
                    if (newuser.Role == "doctor")
                    {
                        newuser.Photo = "8dd25d9051cbef9c3a4518b67a7abdf6.jpg";
                    }

                }
                _context.Add(newuser);
                _context.SaveChanges();
                return true;
            }
        }

        public bool Login(User existuser)
        {
            existuser.Email.ToLower();
            try
            {
                if (!UserExists(existuser.Email))
                {
                    return false;
                }

                else
                {
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
        public bool Verify(User existuser)
        {

            try
            {
                if (!UserExists(existuser.Email))
                {
                    return false;
                }
                else
                {
                    //  return true;

                    User account = GetUserById(existuser.Email);
                    if (account.OTP == existuser.OTP)
                    {
                        account.Verify = true;
                        _context.Attach(account).State = EntityState.Modified;
                        _context.Update(account);
                        _context.SaveChanges();
                        return true;
                    }
                    else
                        return false;


                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }


        }

        public bool ResendCode(User existuser)
        {
            try
            {
                if (!UserExists(existuser.Email))
                {
                    return false;
                }
                else
                {
                    //  return true;

                    User account = GetUserById(existuser.Email);
                  
                    account.OTP = existuser.OTP;
                    account.OTPTime = existuser.OTPTime;
                    _context.Attach(account).State = EntityState.Modified;
                    _context.Update(account);
                    _context.SaveChanges();
                    return true;


                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
        }
        public bool DeleteUser(string Email)
        {
            if (Email == null)
            {
                return true;
            }
            else
            {
                try
                {

                    User account = GetUserById(Email);
                    _context.Remove(account);
                    _context.SaveChanges();
                    return true;
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(Email))
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
        public bool CheckEmailVerify(string Email)
        {
            try
            {
                if (!UserExists(Email))
                {
                    return false;
                }
                else
                {
                    User account = GetUserById(Email);
                    if (account.Verify)
                    {
                        return true;
                    }

                    else
                        return false;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
        }
        public bool ChangePassword(User theuser)
        {
            try
            {
                if (!UserExists(theuser.Email))
                {
                    return false;
                }
                else
                {
                    User account = GetUserById(theuser.Email);
                    string salt = BC.GenerateSalt(12);
                    var password = BC.HashPassword(theuser.Password, salt);
                   account.Password = password;
                    _context.Attach(account).State = EntityState.Modified;
                    _context.Update(account);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;

            }
        }
        public bool UpdateUser(User theuser)
        {
            try
            {
                if (!UserExists(theuser.Email))
                {
                    return false;
                }
                else
                {
                    User account = GetUserById(theuser.Email);
                    account.Fname = theuser.Fname;
                    account.Lname = theuser.Lname;
                    account.PostalCode = theuser.PostalCode;
                    account.Photo = theuser.Photo;
                    account.UnitNumber = theuser.UnitNumber;
                    account.Phone = theuser.Phone;
                    _context.Attach(account).State = EntityState.Modified;
                    _context.Update(account);
                    _context.SaveChanges();
                    return true;
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
        public bool UserExist(string email)
        {
            return _context.Users.Any(e => e.Email == email);
        }

        public int Users()
        {
            List<User> AllUser = new List<User>();
            AllUser = _context.Users.ToList();
            return AllUser.Count;
        }
        public List<User> GetAllUsers()
        {
            List<User> AllUser = new List<User>();
            AllUser = _context.Users.ToList();
            return AllUser;
        }
        public List<User> GetTheElderly()
        {
            List<User> AllUser = new List<User>();
            AllUser = _context.Users.Where(e => e.Role == "elderly").ToList();
            return AllUser;
        }
        public List<User> GetAllDoctors()
        {
            List<User> AllUser = new List<User>();
            AllUser = _context.Users.Where(e => e.Role == "doctor").ToList();
            return AllUser;
        }

    }
}
