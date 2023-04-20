using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackageService.DTO
{
    //public enum Status { NOT_DELIVERED, IN_DELIVERY, DELIVERED }
    public class PackageDto
    {
        public long PackageId { get; set; }
        public long PackageCode { get; set; }
        public string PackageStatus { get; set; }
    }
}
