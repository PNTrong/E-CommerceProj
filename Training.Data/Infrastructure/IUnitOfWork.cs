namespace Training.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}