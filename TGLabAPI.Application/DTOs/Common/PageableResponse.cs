
namespace TGLabAPI.Application.DTOs.Common
{
    public class PageableResponse<T>
    {
        public List<T> Result { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
    }
}