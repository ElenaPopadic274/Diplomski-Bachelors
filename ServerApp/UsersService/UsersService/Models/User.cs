namespace UsersService.Models
{
    public enum DelivererStatus { VERIFIED, NOTVERIFIED, DENIED };
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Type { get; set; }
        public bool Activated { get; set; }
        public DelivererStatus Status { get; set; }
    }
}
