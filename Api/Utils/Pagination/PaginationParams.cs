using System;

namespace Api.Utils.Pagination
{
    public class PaginationParams
    {
        private const int MaxPageSize = 50;

        private int _pageNumber;
        private int _pageSize;

        public PaginationParams()
        {
            // Set default values or leave uninitialized
        }

        public PaginationParams(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = (value < 1) ? 1 : value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value < 1 || value > MaxPageSize) ? 1 : value; }
        }
    }
}
