using System.Collections.Generic;
using System.Linq;
using Training.Data.Infrastructure;
using Training.Model.Models;

namespace Training.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetByTag(string tag, int pageIndex, int pageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetByTag(string tag, int pageIndex, int pageSize, out int totalRow)
        {
            var query = from a in DbContext.Posts
                        join b in DbContext.PostTags
                        on a.ID equals b.PostID
                        where b.TagID == tag
                        select a;

            totalRow = query.Count();

            //example: pagIndex = 1 , pageSize = 20: (1-1)*20=0 => 20:still page 1
            query = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return query;
        }
    }
}