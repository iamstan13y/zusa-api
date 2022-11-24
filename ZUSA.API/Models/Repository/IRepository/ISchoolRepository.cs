using ZUSA.API.Models.Data;

namespace ZUSA.API.Models.Repository.IRepository
{
    public interface ISchoolRepository : IRepository<School>
    {
        //Task<Result<School>> GetSchoolAsync(int Id);
        // Task<Result<IEnumerable<School>>> GetAllSchoolsAsync();
    }
}