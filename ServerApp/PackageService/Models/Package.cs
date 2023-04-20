namespace PackageService.Models
{
    //public enum Status { NOT_DELIVERED, IN_DELIVERY, DELIVERED }
    public class Package
    {
        public long PackageId { get; set; }
        public long PackageCode { get; set; }
        public string PackageStatus { get; set; }
    }
}
