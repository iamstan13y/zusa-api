using ClosedXML.Excel;
using System.Reflection;
using KJColor = System.Drawing.Color;
using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Services
{
    public class ExcelService : IExcelService
    {
        private readonly IConfiguration _configuration;

        public ExcelService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //CAN BE IMPROVED
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

        public async Task<string> GenerateExcelAsync(List<TeamMemberExcelRequest> data)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Sports Register");
            worksheet.Name = "Sports Register";
            worksheet.Cell(1, 1).Value = data[0].SportName?.ToString();
            worksheet.Row(1).Merge();
            worksheet.Row(1).Style.Font.SetBold();
            worksheet.Row(1).Style.Font.SetFontSize(20);
            worksheet.Row(2).Style.Font.SetBold();

            List<PropertyInfo> propertyInfo = typeof(TeamMemberExcelRequest).GetProperties().ToList();
            worksheet.ColumnWidth = 15;

            for (int i = 1; i <= propertyInfo.Count; i++)
            {
                worksheet.Cell(2, i).Value = propertyInfo[i - 1].Name;
                worksheet.Cell(2, i).Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin);
                worksheet.Cell(2, i).Style.Fill.SetBackgroundColor(XLColor.FromColor(KJColor.FromArgb(215, 215, 215)));
            }

            var currentRow = 0;
            foreach (var item in data)
            {
                for (int j = 0; j < propertyInfo.Count; j++)
                {
                    worksheet.Cell(currentRow + 3, j + 1).Value = propertyInfo[j].GetValue(item, null)?.ToString();
                }
                currentRow++;
            }

            var filename = $"zusa-{DateTime.Now.Ticks.ToString()[12..]}-{worksheet.Name}.xlsx";
            using (var stream = File.Create(Path.GetFullPath($"./uploads/{filename}")))
            {
                workbook.SaveAs(stream);
                await stream.DisposeAsync();
            }

            var fileUrl = $"{_configuration["Urls:ProdBaseUrl"]}/uploads/{filename}";

            return await Task.FromResult(fileUrl);
        }
    }
}