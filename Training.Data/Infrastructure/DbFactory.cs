namespace Training.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        //Factory nơi mà mọi thứ được tạo ra.
        private TrainingShopDbContext dbContext;

        public TrainingShopDbContext Init()
        {
            return dbContext ?? (dbContext = new TrainingShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}