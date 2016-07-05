using System;

namespace Training.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        TrainingShopDbContext Init();
    }
}