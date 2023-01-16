namespace ZUSA.API.Models.Local
{
    public class SchoolRequest
    {
        public string? Name { get; set; }
    }

    public class UpdateSchoolRequest : SchoolRequest
    {
        public int Id { get; set; }
    }
}