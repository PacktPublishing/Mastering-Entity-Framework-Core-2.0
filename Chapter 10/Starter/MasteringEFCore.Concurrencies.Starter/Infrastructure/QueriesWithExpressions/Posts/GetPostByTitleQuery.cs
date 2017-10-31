using MasteringEFCore.Concurrencies.Starter.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Models;
using MasteringEFCore.Concurrencies.Starter.Data;
using MasteringEFCore.Concurrencies.Starter.Core.Queries.Posts;
using MasteringEFCore.Concurrencies.Starter.Infrastructure.QueriesWithExpressions.Expressions.Posts;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Concurrencies.Starter.Infrastructure.QueriesWithExpressions.Posts
{
    public class GetPostByTitleQuery : QueryBase, IGetPostByTitleQuery<GetPostByTitleQuery>
    {
        public GetPostByTitleQuery(BlogContext context) : base(context)
        {
        }

        public string Title { get ; set ; }
        public bool IncludeData { get ; set ; }

        public IEnumerable<Post> Handle()
        {
            var expression = new GetPostByTitleQueryExpression
            {
                Title = Title
            };
            return IncludeData
                        ? Context.Posts
                            .Where(expression.AsExpression())
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToList()
                        : Context.Posts
                            .Where(expression.AsExpression())
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            var expression = new GetPostByTitleQueryExpression
            {
                Title = Title
            };
            return IncludeData
                        ? await Context.Posts
                            .Where(expression.AsExpression())
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToListAsync()
                        : await Context.Posts
                            .Where(expression.AsExpression())
                            .ToListAsync();
        }
    }
}
