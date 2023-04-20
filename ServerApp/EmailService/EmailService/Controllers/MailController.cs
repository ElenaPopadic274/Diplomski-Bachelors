using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.Interfaces;
using EmailService.DTO;
using Microsoft.AspNetCore.Authorization;

namespace EmailService.Controllers
{
    [Route("api/emails")]
    [ApiController]
    public class MailController : Controller
    {
        private readonly IMailService _mailService;
        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost]
        public ActionResult Subscribe([FromBody] EmailDto email)
        {
            return Ok(_mailService.Subscribe(email));
        }
        [HttpPut("{packageId}")]
        public ActionResult SendMail(long packageId)
        { 
             return Ok(_mailService.SendMail(packageId)); 
        }
    }
}
