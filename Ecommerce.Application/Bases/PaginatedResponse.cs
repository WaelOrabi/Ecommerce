namespace Ecommerce.Application.Bases
{
    public class PaginatedResponse<T>(bool succeeded = true, List<T> data = default, int count = 0, int page = 1, int pageSize = 10)
    {
        public List<T> Data { get; set; } = data;
        public int CurrentPage { get; set; } = page;
        public int TotalPages { get; set; } = (int)Math.Ceiling(count / (double)pageSize);
        public int TotalCount { get; set; } = count;
        public object Meta { get; set; }
        public int PageSize { get; set; } = pageSize;
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public bool Succeeded { get; set; } = succeeded;

        public static PaginatedResponse<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, count, page, pageSize);
        }
    }
}
