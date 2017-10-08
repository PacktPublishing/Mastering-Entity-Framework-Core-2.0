using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Final.Data;
using MasteringEFCore.Transactions.Final.Core.Queries.Posts;
using MasteringEFCore.Transactions.Final.Core.Queries;
using MasteringEFCore.Transactions.Final.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Transactions.Final.Infrastructure.QueriesWithExpressions.Expressions.Posts
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
