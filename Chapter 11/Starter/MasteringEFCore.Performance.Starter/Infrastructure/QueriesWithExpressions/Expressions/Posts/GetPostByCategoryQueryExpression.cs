using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.Performance.Starter.Data;
using MasteringEFCore.Performance.Starter.Core.Queries.Posts;
using MasteringEFCore.Performance.Starter.Core.Queries;
using MasteringEFCore.Performance.Starter.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Performance.Starter.Infrastructure.QueriesWithExpressions.Expressions.Posts
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
