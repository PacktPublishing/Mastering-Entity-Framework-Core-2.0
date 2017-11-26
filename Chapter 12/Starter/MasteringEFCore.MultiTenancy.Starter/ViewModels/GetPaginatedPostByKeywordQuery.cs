using MasteringEFCore.MultiTenancy.Starter.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Starter.ViewModels
{
    public class GetPaginatedPostByKeywordQuery
    {
        public GetPaginatedPostByKeywordQuery(string keyword, int pageNumber, int pagecount, bool includeData)
        {
            this.Keyword = keyword;
            this.PageNumber = pageNumber;
            this.PageCount = pagecount;
            this.IncludeData = includeData;
        }

        public string Keyword { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
        public bool IncludeData { get; set; }
    }
}
