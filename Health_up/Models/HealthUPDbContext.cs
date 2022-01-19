using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Health_up.Models
{
    public class HealthUPDbContext: DbContext
    {
        private readonly IConfiguration _config;
        public HealthUPDbContext(IConfiguration configuration)
        {
            _config = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = _config.GetConnectionString("MyConn");
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Activity> Activitys { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<MedicalReport> Reports { get; set; }

        //

    }
}
