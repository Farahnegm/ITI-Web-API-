using ITI_Api.DTO;
using Microsoft.EntityFrameworkCore;

namespace ITI_Api.Extentions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedResultDTO<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int page, int pageSize)
        {
            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var items = await query.Skip((page - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PagedResultDTO<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Items = items
            };
        }
    }
}
