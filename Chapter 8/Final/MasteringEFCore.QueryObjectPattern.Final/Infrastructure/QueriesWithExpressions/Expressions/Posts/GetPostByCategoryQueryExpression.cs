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
    public class GetPostByCategoryQueryExpression : IQueryExpression<Post>
    {
        public string Category { get; set; }

        public Expression<Func<Post, bool>> AsExpression()
        {
            return (x => x.Category.Name.ToLower().Contains(Category.ToLower()));
        }
    }
}
