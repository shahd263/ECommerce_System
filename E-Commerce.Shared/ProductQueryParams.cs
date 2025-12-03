namespace E_Commerce.Shared
{
    public class ProductQueryParams
    {
        public int? typeId { get; set; }
        public int? brandId { get; set; }
        public string? Search { get; set; }

        public ProductOrderByOptions? Sort { get; set; }

        private int _pageIndex = 1;

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                _pageIndex = value <= 0 ? 1 : value;
            }
        }
        private const int defaultPageSize = 5;
        private const int MaxSize = 10;
        private  int _PageSize = defaultPageSize;

        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                if (value <= 0)
                    _PageSize = defaultPageSize;

                else if (value > MaxSize)
                    _PageSize = MaxSize;

                else _PageSize = value;
            }
        }
    }
}
