namespace ZUSA.API.Models.Data
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public string? RegNumber { get; set; }
        public string? IdNumber { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public int SchoolId { get; set; }
        public int SportId { get; set; }
        public Sport? Sport { get; set; }
        public School? School { get; set; }
    }
}