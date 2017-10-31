using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Final.Data;
using MasteringEFCore.Concurrencies.Final.Core.Queries.Posts;
using MasteringEFCore.Concurrencies.Final.Core.Queries;
using MasteringEFCore.Concurrencies.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Concurrencies.Final.Infrastructure.QueriesWithExpressions.Expressions.Posts
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
