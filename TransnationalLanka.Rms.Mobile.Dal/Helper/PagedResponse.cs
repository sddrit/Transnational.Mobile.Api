namespace TransnationalLanka.Rms.Mobile.Dal.Helper
{
    public class PagedResponse<T>
    {
        public PagedResponse() { }

        public PagedResponse(IEnumerable<T> data, int _pageNumber, int _pageSize, int _totalCount)
        {
            Data = data;
            pageNumber = _pageNumber;
            pageSize = _pageSize;
            totalCount = _totalCount;
            totalPages = (int)Math.Ceiling(_totalCount / (double)_pageSize);
        }

        public IEnumerable<T> Data { get; set; }

        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }
        public int? totalPages { get; set; }

    }
}

