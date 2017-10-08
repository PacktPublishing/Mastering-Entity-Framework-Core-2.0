using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.ViewModels
{
    public class PostDetailQuery
    {
        public PostDetailQuery(int? id)
        {
            this.Id = id;
        }

        public int? Id { get; set; }
    }
}
