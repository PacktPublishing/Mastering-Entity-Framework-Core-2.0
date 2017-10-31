using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Concurrencies.Starter.Data;
using MasteringEFCore.Concurrencies.Starter.Core.Queries.Posts;
using MasteringEFCore.Concurrencies.Starter.Core.Queries;
using MasteringEFCore.Concurrencies.Starter.Infrastructure.QueriesWithExpressions.Expressions.Posts;
using MasteringEFCore.Concurrencies.Starter.Models;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.Concurrencies.Starter.Infrastructure.QueriesWithExpressions.Posts
{
    public class GetPostByPublishedYearQuery : QueryBase, IGetPostByPublishedYearQuery<GetPostByPublishedYearQuery>
    {
        public GetPostByPublishedYearQuery(BlogContext context) : base(context)
        {
        }

        public int Year { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            var expression = new GetPostByPublishedYearQueryExpression
            {
                Year = Year
            };
            return IncludeData
                        ? Context.Posts
                            .Where(expression.AsExpression())
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToList()
                        : Context.Posts
                            .Where(expression.AsExpression())
                            .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            var expression = new GetPostByPublishedYearQueryExpression
            {
                Year = Year
            };
            return IncludeData
                        ? await Context.Posts
                            .Where(expression.AsExpression())
                            .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                            .ToListAsync()
                        : await Context.Posts
                            .Where(expression.AsExpression())
                            .ToListAsync();
        }
    }
}
