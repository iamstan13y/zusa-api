using ZUSA.API.Models.Data;

namespace ZUSA.API.Services
{
    public interface IExcelService
    {
        Task<IEnumerable<TeamMember>> ExtractRecordsAsync(IFormFile excelFile);
    }
}