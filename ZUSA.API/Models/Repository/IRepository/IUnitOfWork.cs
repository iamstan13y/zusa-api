namespace ZUSA.API.Models.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISchoolRepository School {get;}
        ISportRepository Sport {get;}
        void SaveChanges();
    }
}