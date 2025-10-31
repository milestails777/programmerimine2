using System;
using System.Collections.Generic;

namespace KooliProjekt.Application.Infrastructure.Paging
{
    public class PagedResult<T>
    {
        public IList<T> Items { get; }
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

        public PagedResult(IList<T> items, int totalCount, int page, int pageSize)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
        }
    }
}