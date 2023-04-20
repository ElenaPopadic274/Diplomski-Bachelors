using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Models
{
    public class Mail
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public long PackageId { get; set; }
    }
}
