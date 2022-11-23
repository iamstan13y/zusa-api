namespace ZUSA.API.Models.Local
{
    public class TeamMembersRequest
    {
        public int SportId { get; set; }
        public int SchoolId { get; set; }
        public IFormFile? TeamExcelFile { get; set; }
    }
}