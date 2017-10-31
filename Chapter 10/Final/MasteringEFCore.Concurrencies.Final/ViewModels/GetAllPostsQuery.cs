using MasteringEFCore.Concurrencies.Final.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Concurrencies.Final.ViewModels
{
    public class GetAllPostsQuery
    {
        public GetAllPostsQuery(bool includeData)
        {
            this.IncludeData = includeData;
        }

        public bool IncludeData { get; set; }
    }
}
