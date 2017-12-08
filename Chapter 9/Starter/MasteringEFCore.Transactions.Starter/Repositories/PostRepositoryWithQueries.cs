using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.Transactions.Starter.Models;
using MasteringEFCore.Transactions.Starter.Data;
using Microsoft.EntityFrameworkCore;
using MasteringEFCore.Transactions.Starter.ViewModels;
using MasteringEFCore.Transactions.Starter.Handlers;

namespace MasteringEFCore.Transactions.Starter.Repositories
{
    public class PostRepositoryWithQueries : IPostRepositoryWithQueries
    {
        private readonly BlogContext _context;

        public PostRepositoryWithQueries(BlogContext context)
        {
            _context = context;
        }

        public IEnumerable<Post> Get<T>(T query)
            where T : class
        {
            switch(typeof(T).Name)
            {
                case "GetPaginatedPostByKeywordQuery":
                    var getPaginatedPostByKeywordQueryHandler = new GetPaginatedPostByKeywordQueryHandler(_context);
                    return getPaginatedPostByKeywordQueryHandler.Handle(query as GetPaginatedPostByKeywordQuery);
                case "GetPostByAuthorQuery":
                    var getPostByAuthorQueryHandler = new GetPostByAuthorQueryHandler(_context);
                    return getPostByAuthorQueryHandler.Handle(query as GetPostByAuthorQuery);
                case "GetPostByCategoryQuery":
                    var getPostByCategoryQueryHandler = new GetPostByCategoryQueryHandler(_context);
                    return getPostByCategoryQueryHandler.Handle(query as GetPostByCategoryQuery);
                case "GetPostByHighestVisitorsQuery":
                    var getPostByHighestVisitorsQueryHandler = new GetPostByHighestVisitorsQueryHandler(_context);
                    return getPostByHighestVisitorsQueryHandler.Handle(query as GetPostByHighestVisitorsQuery);
                case "GetPostByPublishedYearQuery":
                    var getPostByPublishedYearQueryHandler = new GetPostByPublishedYearQueryHandler(_context);
                    return getPostByPublishedYearQueryHandler.Handle(query as GetPostByPublishedYearQuery);
                case "GetPostByTitleQuery":
                    var getPostByTitleQueryHandler = new GetPostByTitleQueryHandler(_context);
                    return getPostByTitleQueryHandler.Handle(query as GetPostByTitleQuery);
                default:
                    var getAllPostsQueryHandler = new GetAllPostsQueryHandler(_context);
                    return getAllPostsQueryHandler.Handle(query as GetAllPostsQuery);
            }
        }

        public async Task<IEnumerable<Post>> GetAsync<T>(T query)
            where T : class
        {
            switch (typeof(T).Name)
            {
                case "GetPaginatedPostByKeywordQuery":
                    var getPaginatedPostByKeywordQueryHandler = new GetPaginatedPostByKeywordQueryHandler(_context);
                    return await getPaginatedPostByKeywordQueryHandler.HandleAsync(query as GetPaginatedPostByKeywordQuery);
                case "GetPostByAuthorQuery":
                    var getPostByAuthorQueryHandler = new GetPostByAuthorQueryHandler(_context);
                    return await getPostByAuthorQueryHandler.HandleAsync(query as GetPostByAuthorQuery);
                case "GetPostByCategoryQuery":
                    var getPostByCategoryQueryHandler = new GetPostByCategoryQueryHandler(_context);
                    return await getPostByCategoryQueryHandler.HandleAsync(query as GetPostByCategoryQuery);
                case "GetPostByHighestVisitorsQuery":
                    var getPostByHighestVisitorsQueryHandler = new GetPostByHighestVisitorsQueryHandler(_context);
                    return await getPostByHighestVisitorsQueryHandler.HandleAsync(query as GetPostByHighestVisitorsQuery);
                case "GetPostByPublishedYearQuery":
                    var getPostByPublishedYearQueryHandler = new GetPostByPublishedYearQueryHandler(_context);
                    return await getPostByPublishedYearQueryHandler.HandleAsync(query as GetPostByPublishedYearQuery);
                case "GetPostByTitleQuery":
                    var getPostByTitleQueryHandler = new GetPostByTitleQueryHandler(_context);
                    return await getPostByTitleQueryHandler.HandleAsync(query as GetPostByTitleQuery);
                default:
                    var getAllPostsQueryHandler = new GetAllPostsQueryHandler(_context);
                    return await getAllPostsQueryHandler.HandleAsync(query as GetAllPostsQuery);
            }
        }

        public Post GetSingle<T>(T query)
            where T : class
        {
            //switch (typeof(T).Name)
            //{
            //    case "GetPostByIdQuery":
            //        var getPostByIdQueryHandler = new GetPostByIdQueryHandler(_context);
            //        return getPostByIdQueryHandler.Handle(query as GetPostByIdQuery);
            //}

            var getPostByIdQueryHandler = new GetPostByIdQueryHandler(_context);
            return getPostByIdQueryHandler.Handle(query as GetPostByIdQuery);
        }

        public async Task<Post> GetSingleAsync<T>(T query)
            where T : class
        {
            //switch (typeof(T).Name)
            //{
            //    case "GetPostByIdQuery":
            //        var getPostByIdQueryHandler = new GetPostByIdQueryHandler(_context);
            //        return await getPostByIdQueryHandler.HandleAsync(query as GetPostByIdQuery);
            //}

            var getPostByIdQueryHandler = new GetPostByIdQueryHandler(_context);
            return await getPostByIdQueryHandler.HandleAsync(query as GetPostByIdQuery);
        }
    }
}
