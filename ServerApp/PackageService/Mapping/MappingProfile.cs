using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackageService.DTO;
using PackageService.Models;

namespace PackageService.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Package, PackageDto>().ReverseMap();
        }
    }
}
