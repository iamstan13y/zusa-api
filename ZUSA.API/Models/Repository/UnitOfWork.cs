using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Services;

namespace ZUSA.API.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public ISchoolRepository School { get; private set; }
        
        public UnitOfWork(AppDbContext context)
        {
            School = new SchoolRepository(context);
            _context = context;
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}