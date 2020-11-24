using System.Linq;

namespace MjCommerce.Shared.Filters.Base
{
    public class PagingFilter<T> : FilterBase
    {
        const int maxPageSize = 8;

        public int TotalCount { get; set; }
        public int PageNumber { get; set; } = 1;

        int _pageSize = maxPageSize;

        public int PageSize
        {
            get => _pageSize;

            set => _pageSize = value > maxPageSize ? maxPageSize : value;
        }

        public IQueryable<T> Build(IQueryable<T> initialSet)
        {
            return initialSet.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        }
    }
}