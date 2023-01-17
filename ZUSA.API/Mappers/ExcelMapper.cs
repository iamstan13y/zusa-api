using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Mappers
{
    public static class ExcelMapper
    {
        public static List<TeamMemberExcelRequest> ToExcelRequest(this List<TeamMember> teamMembers)
        {
            List<TeamMemberExcelRequest> response = new();
            teamMembers.ForEach(member =>
            {
                response.Add(new TeamMemberExcelRequest
                {
                    DOB = member.DOB.Date,
                    FirstName = member.FirstName,
                    Gender = member.Gender,
                    IdNumber = member.IdNumber,
                    LastName = member.LastName,
                    RegNumber = member.RegNumber?.ToUpper(),
                    SchoolName = member.Subscription?.School?.Name,
                    SportName = member.Subscription?.Sport?.Name
                });
            });

            return response;
        }
    }
}