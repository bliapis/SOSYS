using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LT.SO.Site.Models
{
    public class DataTableAjaxPostModel
    {
        private List<Order> order { get; set; }
        private int start { get; set; }
        private int length { get; set; }

        public int draw { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }

        [JsonProperty("order")]
        public List<Order> Order {
            get { return order; }
            set {
                if (value != null && value.Count > 0)
                {
                    SortColumn = value.FirstOrDefault().column;
                    SorDirect = value.FirstOrDefault().dir;
                }
            }
        }

        [JsonProperty("start")]
        public int Start
        {
            get { return start; }
            set
            {
                Skip = value;
            }
        }

        [JsonProperty("length")]
        public int Length
        {
            get { return length; }
            set
            {
                PageSize = value;
            }
        }

        public int Skip { get; set; }

        public int PageSize { get; set; }

        public int SortColumn { get; set; }
        public string SorDirect { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}