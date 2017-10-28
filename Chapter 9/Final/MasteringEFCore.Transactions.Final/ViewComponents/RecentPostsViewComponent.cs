using MasteringEFCore.Transactions.Final.Data;
using MasteringEFCore.Transactions.Final.Infrastructure.Queries.Posts;
using MasteringEFCore.Transactions.Final.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.Transactions.Final.ViewComponents
{
    public class RecentPostsViewComponent : ViewComponent
    {
        private readonly BlogContext _context;
        private readonly IPostRepository _postRepository;
        public RecentPostsViewComponent(IPostRepository postRepository,
            BlogContext context)
        {
            _postRepository = postRepository;
            _context = context;
        }

        public IViewComponentResult Invoke(int size)
        {
            return View(_postRepository.Get(
                new GetRecentPostQuery(_context)
                {
                    IncludeData = true,
                    Size = size
                }));
        }
    }
}
