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
        public int SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
    }
}