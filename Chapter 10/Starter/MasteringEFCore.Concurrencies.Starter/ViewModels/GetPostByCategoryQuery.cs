using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Starter.ViewModels
{
    public class GetPostByCategoryQuery
    {
        public GetPostByCategoryQuery(string category, bool includeData)
        {
            this.Category = category;
            this.IncludeData = includeData;
        }

        public string Category { get; set; }
        public bool IncludeData { get; set; }
    }
}
