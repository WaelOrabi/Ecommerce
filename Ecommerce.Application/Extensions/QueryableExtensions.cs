using Ecommerce.Application.Bases;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResponse<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null)
            {
                throw new Exception("Empty");
            }
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;
            int count = await source.AsNoTracking().CountAsync();
            if (count == 0)
                return PaginatedResponse<T>.Success(new List<T>(), count, pageNumber, pageSize);
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return PaginatedResponse<T>.Success(items, count, pageNumber, pageSize);
        }
    }
}
