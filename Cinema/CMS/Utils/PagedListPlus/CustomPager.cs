using System.Collections.Generic;

namespace CMS.Utils.PagedListPlus
{
    public class CustomPager<TEntity> : PagedList<TEntity>
        where TEntity : class
    {
        public CustomPager(IEnumerable<TEntity> items, int page, int pageSize, int totalCount)
            : base(items, page, pageSize, totalCount)
        {
            
        }
    }
}
