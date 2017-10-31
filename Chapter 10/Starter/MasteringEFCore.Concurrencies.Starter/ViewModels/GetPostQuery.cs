using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.ViewModels
{
    public class GetPostQuery
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public bool IncludeData { get; set; }
        public DateTime PublishedDateTime { get; set; }
        public string Keyword { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool HighestVisitors { get; set; }
    }
}
