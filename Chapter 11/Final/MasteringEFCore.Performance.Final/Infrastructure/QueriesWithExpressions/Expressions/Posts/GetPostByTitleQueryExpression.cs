using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.Performance.Final.Data;
using MasteringEFCore.Performance.Final.Core.Queries.Posts;
using MasteringEFCore.Performance.Final.Core.Queries;
using MasteringEFCore.Performance.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Performance.Final.Infrastructure.QueriesWithExpressions.Expressions.Posts
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
