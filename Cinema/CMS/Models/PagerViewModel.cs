using System.Collections.Generic;

namespace CMS.Models
{
    public class PagerViewModel<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public Pager Pager { get; set; }
    }
}
