using ZUSA.API.Models.Data;
using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;

namespace ZUSA.API.Models.Repository
{
    public class SportRepository : Repository<Sport>, ISportRepository
    {
        public SportRepository(AppDbContext context) : base(context)
        {
        }
    }
}