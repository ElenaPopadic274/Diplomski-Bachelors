using AutoMapper;
using PackageService.DTO;
using PackageService.Infrastructure;
using PackageService.Interfaces;
using PackageService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PackageService.Services
{
    public class PackageService : IPackageService
    {
        private readonly IMapper _mapper;
        private readonly PackagesDbContext _dbContext;

        public PackageService(IMapper mapper, PackagesDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public bool AddEntity(PackageDto entity)
        {
            try
            {
                Package packet = _mapper.Map<Package>(entity);
                _dbContext.Packages.Add(packet);
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }

            return true;
        }
        public long GetWithId(PackageDto packet)
        {
            var tmp = _dbContext.Packages.Where(s => s.PackageId == packet.PackageId).Where(s => s.PackageStatus == packet.PackageStatus).Where(s => s.PackageCode == packet.PackageCode);
            return _mapper.Map<PackageDto>(tmp.ToList()[0]).PackageId;

        }
        public PackageDto FindById(long id)
        {
            return _mapper.Map<PackageDto>(_dbContext.Packages.Find(id));
        }

        public List<PackageDto> GetAll()
        {
            return _mapper.Map<List<PackageDto>>(_dbContext.Packages.ToList());

        }

        public bool ChangeStatus(PackageDto entity)
        {
            Package packet = _dbContext.Packages.FirstOrDefault(x => x.PackageCode == entity.PackageCode);
            if (packet == null)
                return false;
            if(packet.PackageStatus == entity.PackageStatus)
            {
                return false;
            }
            packet.PackageStatus = entity.PackageStatus;
            _dbContext.SaveChanges();
            if (packet.PackageStatus == "DELIVERED")
            {
                return SendMail(packet.PackageCode);
            }
            return true;
        }

        private bool SendMail(long code)
        {
            HttpClient httpClient = new HttpClient { BaseAddress = new("https://localhost:44377") };
            var response = httpClient.PutAsync("/api/emails/" + code, null).Result;
            return response.IsSuccessStatusCode;
        }

        public bool RemoveEntity(long id)
        {
            Package packet = _dbContext.Packages.Find(id);
            if (packet == null)
                return false;
            _dbContext.Packages.Remove(packet);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
