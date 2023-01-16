namespace ZUSA.API.Models.Local
{
    public class SportRequest
    {
        public string? Name { get; set; }
        public int TeamMemberLimit { get; set; }
        public DateTime Deadline { get; set; }
    }

    public class UpdateSportRequest : SportRequest
    {
        public int Id { get; set; }
    }
}