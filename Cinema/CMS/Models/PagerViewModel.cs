using CMS.Utils.PagedListPlus;
using System.Collections.Generic;

namespace CMS.Models
{
    public class PagerViewModel<T> : CustomPager<T>
        where T : class
    {
        public PagerViewModel(
            IEnumerable<T> items,
            int page,
            int size,
            int count)
            : base(items, page, size, count)
        {
            Subset.AddRange(items);
        }

        public List<T> Items { get; set; } = new List<T>();
        public Pager Pager { get; set; }
    }
}
