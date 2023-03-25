namespace ZUSA.API.Models.Data
{
    public class Subscription
    {
        public int Id { get; set; }
        public int SportId { get; set; }
        public int SchoolId { get; set; }
        public char Gender { get; set; }
        public School? School { get; set; }
        public Sport? Sport { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}