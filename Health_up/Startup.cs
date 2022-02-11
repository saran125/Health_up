using AspNetCore.ReCaptcha;
using Health_up.Models;
using Health_up.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Health_up
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddTransient<UserService>();
            services.AddTransient<AppointmentService>();
            services.AddTransient<BookingService>();
            services.AddTransient<ReportService>();
            services.AddTransient<ActivityService>();
            services.AddTransient<DoctorTimeslotService>();
            services.AddTransient<ActivityFeedbackService>();
            services.AddTransient<IMailService, EmailSender>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                    options.SlidingExpiration = true;
                    options.AccessDeniedPath = "/forbidden";
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                });
            services.AddDbContext<HealthUPDbContext>();
            services.AddReCaptcha(Configuration.GetSection("ReCaptcha"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.Use(async (context, next) =>
            {
               
                if (context.Response.StatusCode == 404)
                {

                    context.Request.Path = "/NotFound";
                }
                await next();

            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
         
            app.UseAuthorization();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
