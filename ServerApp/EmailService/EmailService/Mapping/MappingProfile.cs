using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailService.DTO;
using EmailService.Models;

namespace EmailService.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Mail, EmailDto>().ReverseMap();
        }
    }
}
