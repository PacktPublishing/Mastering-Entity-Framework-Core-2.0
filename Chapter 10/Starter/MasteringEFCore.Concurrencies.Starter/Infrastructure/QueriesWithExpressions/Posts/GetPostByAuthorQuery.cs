﻿using System;
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
    public class GetPostByAuthorQuery : QueryBase, IGetPostByAuthorQuery<GetPostByAuthorQuery>
    {
        public GetPostByAuthorQuery(BlogContext context) : base(context)
        {
        }

        public string Author { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Post> Handle()
        {
            var expression = new GetPostByAuthorQueryExpression
            {
                Author = Author
            };
            return IncludeData
                ? Context.Posts
                    .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .ToList()
                : Context.Posts
                    .Include(p => p.Author)
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .ToList();
        }

        public async Task<IEnumerable<Post>> HandleAsync()
        {
            var expression = new GetPostByAuthorQueryExpression
            {
                Author = Author
            };
            return IncludeData
                ? await Context.Posts
                    .Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category)
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .ToListAsync()
                : await Context.Posts
                    .Include(p => p.Author)
                    .AsQueryable()
                    .Where(expression.AsExpression())
                    .ToListAsync();
        }
    }
}
