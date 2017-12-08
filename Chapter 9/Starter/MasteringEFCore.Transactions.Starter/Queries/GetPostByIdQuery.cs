using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Starter.Queries
{
    public class GetPostByIdQuery
    {
        public GetPostByIdQuery(int? id, bool includeData)
        {
            this.Id = id;
            this.IncludeData = includeData;
        }

        public int? Id { get; set; }
        public bool IncludeData { get; set; }
    }
}
