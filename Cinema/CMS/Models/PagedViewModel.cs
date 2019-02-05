using CMS.Utils.Pager;
using System.Collections.Generic;

namespace CMS.Models
{
    public class PagedViewModel<TModel> : PagedListAdapter<TModel>
        where TModel : class
    {
        public PagedViewModel(
            List<TModel> items,
            int page,
            int size,
            string orderBy,
            bool isAscending,
            string filter,
            bool isExact,
            int count)
            : base(page, size, count)
        {
            Items = items;
            Page = page;
            Size = size;
            OrderBy = orderBy;
            IsAsc = isAscending;
            Filter = filter;
            IsExact = isExact;
            Count = count;
        }

        public new List<TModel> Items { get; set; } = new List<TModel>();
        public int Page { get; set; }
        public int Size { get; set; }
        public string OrderBy { get; set; }
        public bool IsAsc { get; set; }
        public string Filter { get; set; }
        public bool IsExact { get; set; }
        public new int Count { get; set; }
    }
}
