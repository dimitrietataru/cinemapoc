using Movies = Core.Models;
using System.Collections.Generic;
using CMS.Utils.PagedListPlus;

namespace CMS.Models.Movie
{
    public class MovieIndexViewModel : CustomPager<Movies.Movie>
    {
        public MovieIndexViewModel(
            IEnumerable<Movies.Movie> items,
            int page, int pageSize, int totalCount)
            : base(items, page, pageSize, totalCount)
        {
            TotalCount = totalCount;
        }

        public string OrderBy { get; set; }
        public bool OrderDesc { get; set; }
        public int TotalCount { get; set; }
        public string MatchString { get; set; }
    }
}
