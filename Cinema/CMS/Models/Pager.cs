namespace CMS.Models
{
    public class Pager
    {
        public int page;
        public int size;
        public string orderBy;
        public bool order;
        public int count;

        public Pager(
            int page,
            int size,
            string orderBy,
            bool order,
            int count)
        {
            this.page = page;
            this.size = size;
            this.orderBy = orderBy;
            this.order = order;
            this.count = count;
        }
    }
}
