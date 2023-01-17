using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Services
{
    public interface IExcelService
    {
        Task<IEnumerable<TeamMember>> ExtractRecordsAsync(IFormFile excelFile);
        Task<string> GenerateExcelAsync(List<TeamMemberExcelRequest> data);
    }
}