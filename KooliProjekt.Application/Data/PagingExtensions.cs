using System;
using System.Linq;
using System.Threading.Tasks;
using KooliProjekt.Application.Infrastructure.Paging;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Data
{
    public static class PagerExtension
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            page = Math.Max(page, 1);
            if (pageSize == 0) pageSize = 10;

            var totalCount = await query.CountAsync();

            var skip = (page - 1) * pageSize;
            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<T>(
                items: items,
                totalCount: totalCount,
                page: page,
                pageSize: pageSize
            );
        }
    }
}