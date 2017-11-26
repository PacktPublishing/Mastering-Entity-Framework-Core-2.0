using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.MultiTenancy.Final.Data;
using MasteringEFCore.MultiTenancy.Final.Core.Queries.Posts;
using MasteringEFCore.MultiTenancy.Final.Core.Queries;
using MasteringEFCore.MultiTenancy.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.MultiTenancy.Final.Infrastructure.QueriesWithExpressions.Expressions.Posts
{
    public class GetPostByTitleQueryExpression : IQueryExpression<Post>
    {
        public string Title { get ; set ; }

        public Expression<Func<Post, bool>> AsExpression()
        {
            return (x => x.Title.ToLower().Contains(Title.ToLower()));
        }
    }
}
