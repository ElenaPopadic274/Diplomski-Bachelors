using AutoMapper;
using EmailService.DTO;
using EmailService.Infrastructure;
using EmailService.Interfaces;
using EmailService.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace EmailService.Services
{
    public class MailService : IMailService
    {
        private readonly IMapper _mapper;
        private readonly MailDbContext _dbContext;

        public MailService(IMapper mapper, MailDbContext dbContext, IConfiguration config)
        {
            _mapper = mapper;
            _dbContext = dbContext;

        }

        public bool SendMail(long packageId)
        {
            List<string> mails = _dbContext.Emails.Where(x => x.PackageId == packageId).Select(x => x.Email).ToList();
            if (!mails.Any())
                return false;
            foreach (var mail in mails)
            {
                SendMail(mail);
            }
            return true;
        }
         
        public bool Subscribe(EmailDto emailDto)
        {
            try
            {
                Mail mail = new() { Email = emailDto.Email, PackageId = emailDto.PackageId };
                _dbContext.Emails.Add(mail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            } 
        }

        private void SendMail(string to)
        {
            string from = "elenapopadic@gmail.com"; //From address    
            MailMessage message = new MailMessage(from, to); 

            message.Subject = "Subscription";
            message.Body = "Package is delivered";
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = false;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("elenapopadic@gmail.com", "dndkphbuzimcyswv");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            try
            {
                client.Send(message);
            }

            catch (SmtpException ex)
            {
            }
        }
    }
}
