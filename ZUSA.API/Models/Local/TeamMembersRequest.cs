namespace ZUSA.API.Models.Local
{
    public class TeamMembersRequest
    {
        public int SubscriptionId { get; set; }
        public IFormFile? TeamExcelFile { get; set; }
    }
}