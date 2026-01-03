using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API.RequestHelpers
{
    // we using primary constructor instead of defining a constructor inside the class
    public class Pagination<T>(int pageIndex, int pageSize, int count, IReadOnlyList<T> data) where T : class
    {
        public int PageIndex { get; set; } = pageIndex;
        public int PageSize { get; set; } = pageSize;
        public int Count { get; set; } = count;
        public IReadOnlyList<T> Data { get; set; } = data;
    }
}
