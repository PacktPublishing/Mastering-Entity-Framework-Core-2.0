using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.MultiTenancy.Final.ViewModels
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
