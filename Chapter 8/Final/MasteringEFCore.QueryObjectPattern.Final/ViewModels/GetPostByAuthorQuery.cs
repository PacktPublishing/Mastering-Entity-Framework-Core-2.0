using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.ViewModels
{
    public class GetPostByAuthorQuery
    {
        public GetPostByAuthorQuery(string author, bool includeData)
        {
            this.Author = author;
            this.IncludeData = includeData;
        }

        public string Author { get; set; }
        public bool IncludeData { get; set; }
    }
}
