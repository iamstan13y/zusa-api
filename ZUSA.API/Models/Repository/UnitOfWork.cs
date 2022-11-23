using ZUSA.API.Models.Local;
using ZUSA.API.Models.Repository.IRepository;
using ZUSA.API.Services;

namespace ZUSA.API.Models.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        //public IEventTypeRepository EventType { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            //EventType = new EventTypeRepository(context);
            _context = context;
        }

        public void SaveChanges() => _context.SaveChanges();
    }
}