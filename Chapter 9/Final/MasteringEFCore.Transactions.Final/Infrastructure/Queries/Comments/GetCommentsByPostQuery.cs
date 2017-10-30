using MasteringEFCore.Transactions.Final.Core.Queries.Comments;
using MasteringEFCore.Transactions.Final.Data;
using MasteringEFCore.Transactions.Final.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.Infrastructure.Queries.Comments
{
    public class GetCommentsByPostQuery
        : QueryBase, IGetCommentsByPostQuery<GetCommentsByPostQuery>
    {
        public GetCommentsByPostQuery(BlogContext context) : base(context)
        {
        }

        public int PostId { get; set; }
        public bool IncludeData { get; set; }

        public IEnumerable<Comment> Handle()
        {
            var data = IncludeData
                ? Context.Comments
                    .Where(comment => comment.PostId.Equals(PostId))
                    .Include(p => p.Person)
                    .Include(p => p.User)
                    .ToList()
                : Context.Comments
                    .ToList();

            data.ForEach(item =>
            {
                item.Person = item.Person ?? new Person();
                item.User = item.User ?? new User();
            });

            return data;
        }

        public async Task<IEnumerable<Comment>> HandleAsync()
        {
            var data = IncludeData
                ? await Context.Comments
                    .Where(comment => comment.PostId.Equals(PostId))
                    .Include(p => p.Person)
                    .Include(p => p.User)
                    .ToListAsync()
                : await Context.Comments
                    .ToListAsync();

            data.ForEach(item =>
            {
                item.Person = item.Person ?? new Person();
                item.User = item.User ?? new User();
            });

            return data;
        }
    }
}
