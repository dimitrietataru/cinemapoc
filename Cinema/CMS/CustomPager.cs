using System.Collections.Generic;

namespace CMS
{
    public class CustomPager<T> : CustomPagedList<T>
        where T : class
    {
        public CustomPager(IEnumerable<T> items, int page, int pageSize, int totalCount)
            : base(items, page, pageSize, totalCount)
        {
            PageNumber = page;
        }
    }
}
