using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackageService.DTO;
using PackageService.Models;

namespace PackageService.Interfaces
{
    public interface IPackageService
    {
        List<PackageDto> GetAll();
        PackageDto FindById(long id);
        bool AddEntity(PackageDto entity);
        bool ChangeStatus(PackageDto entity);
        long GetWithId(PackageDto product); 
    }
}
