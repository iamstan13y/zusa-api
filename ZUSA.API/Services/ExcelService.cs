using ClosedXML.Excel;
using System.ComponentModel;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Services
{
    public class ExcelService : IExcelService
    {
        public async Task<IEnumerable<TeamMember>> ExtractRecordsAsync(IFormFile excelFile)
        {
            using var workbook = new XLWorkbook(excelFile.OpenReadStream());
            
            var worksheet = workbook.Worksheets.First();
            int noOfColumns = worksheet.LastColumnUsed().ColumnNumber();
            int noOfRows = worksheet.LastRowUsed().RowNumber();
            List<TeamMember> teamMembers = new();
            
            for (int rowIterator = 2; rowIterator <= noOfRows; rowIterator++)
            {
                var member = new TeamMember
                {
                    FirstName = worksheet.Cell(rowIterator, 1).Value?.ToString(),
                    LastName = worksheet.Cell(rowIterator, 2).Value?.ToString(),
                    DOB = Convert.ToDateTime(worksheet.Cell(rowIterator, 3).Value?.ToString()),
                    Gender = worksheet.Cell(rowIterator, 4).Value?.ToString(),
                    RegNumber = worksheet.Cell(rowIterator, 5).Value?.ToString(),
                    IdNumber = worksheet.Cell(rowIterator, 6).Value?.ToString(),
                };

                teamMembers.Add(member);
            }

            return await Task.FromResult(teamMembers);
        }
    }
}