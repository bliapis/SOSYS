namespace LT.SO.Domain.Core.DTO
{
    public class BaseFilterDTO
    {
        public int Skip { get; set; }
        public int PageSize { get; set; }
        public int SortColumn { get; set; }
        public string SorDirect { get; set; }
    }
}