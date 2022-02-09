using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Health_up.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Health_up.Services
{
   
        public interface IMailService
        {
            Task SendEmailAsync(string emailAddress, string name, string content);
        }

        public class EmailSender : IMailService
        {
            private readonly IConfiguration _config;
            private static string ApiKey;

            public EmailSender(IConfiguration configuration)
            {
                _config = configuration;
                ApiKey = _config["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"];
            }

            public async Task SendEmailAsync(string emailAddress, string name, string content)
            {
                Console.WriteLine(ApiKey);
                var client = new SendGridClient(ApiKey);
                var from = new EmailAddress("healthupnyp@gmail.com", "OTP");
                var subject = "Your OTP";
                var to = new EmailAddress("kumarsaran522@gmail.com", name);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
                var response = await client.SendEmailAsync(msg);
            }
        }

   
}
