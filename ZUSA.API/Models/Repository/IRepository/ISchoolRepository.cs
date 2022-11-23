using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface ISchoolRepository : IRepository<School>
    {
       //Task<Result<School>> GetSchoolAsync(int Id);
       // Task<Result<IEnumerable<School>>> GetAllSchoolsAsync();
    }
}