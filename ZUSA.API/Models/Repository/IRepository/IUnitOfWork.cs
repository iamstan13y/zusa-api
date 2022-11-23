namespace ZUSA.API.Models.Repository.IRepository
{
    public interface IUnitOfWork
    {
        void SaveChanges();
    }
}