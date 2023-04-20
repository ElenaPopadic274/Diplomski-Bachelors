using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.DTO;
using EmailService.Models;

namespace EmailService.Interfaces
{
    public interface IMailService
    {
        bool Subscribe(EmailDto emailDto);
        bool SendMail(long packageId);
    }
}
