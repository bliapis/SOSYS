namespace LT.SO.Site.Models
{
    public class BaseFilter
    {
        public int Skip { get; set; }
        public int PageSize { get; set; }
        public int SortColumn { get; set; }
        public string SorDirect { get; set; }
    }
}