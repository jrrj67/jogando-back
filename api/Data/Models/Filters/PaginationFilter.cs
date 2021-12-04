namespace JogandoBack.API.Data.Models.Filters
{
    public class PaginationFilter
    {
        private const int _pageMaxSize = 100;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public PaginationFilter()
        {
        }

        public PaginationFilter(int pageNumber = 1, int pageSize = 10)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > _pageMaxSize ? 10 : pageSize;
        }
    }
}
