using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using MasteringEFCore.QueryObjectPattern.Final.Core.Queries.Posts;
using MasteringEFCore.QueryObjectPattern.Final.Core.Queries;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.QueryObjectPattern.Final.Infrastructure.QueriesWithExpressions.Expressions.Posts
{
    public class GetPostByAuthorQueryExpression : IQueryExpression<Post>
    {
        public string Author { get; set; }

        public Expression<Func<Post, bool>> AsExpression()
        {
            return (x => x.Category.Name.ToLower().Contains(Author.ToLower()));
        }
    }
}
