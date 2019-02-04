using PagedList.Core;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CMS.Utils.Pager
{
    public class PagedListAdapter<T> : PagedListMetaData, IPagedList<T>
        where T : class
    {
        public PagedListAdapter(int page, int size, int count)
        {
            page = Math.Max(page, 1);
            size = Math.Max(size, 1);

            PageNumber = page;
            PageSize = size;
            TotalItemCount = count;
            PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
            HasPreviousPage = PageNumber > 1;
            HasNextPage = PageNumber < PageCount;
            IsFirstPage = PageNumber == 1;
            IsLastPage = PageNumber >= PageCount;
            FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
            var lastItemIndex = FirstItemOnPage + PageSize - 1;
            LastItemOnPage = Math.Min(lastItemIndex, TotalItemCount);
        }

        public List<T> Items { get; set; } = new List<T>();

        public T this[int index] => Items[index];

        public int Count => Items.Count;

        public IPagedList GetMetaData() => new PagedListMetaData(this);

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
