using MasteringEFCore.QueryObjectPattern.Final.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasteringEFCore.QueryObjectPattern.Final.Repositories
{
    public interface IPostRepository
    {
        IEnumerable<Post> GetAllPosts();
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Post GetPostById(int? id, bool includeData = true);
        Task<Post> GetPostByIdAsync(int? id, bool includeData = true);
        IEnumerable<Post> FindPostByTitle(string title);
        IEnumerable<Post> FindPostByAuthor(string author);
        IEnumerable<Post> FindPostByPublishedYear(int year);
        IEnumerable<Post> FindPostByHighestVisitors();
        IEnumerable<Post> FindPostByCategory(string category);
        IEnumerable<Post> FindPost(string keyword, int pageNumber, int pageCount);
        int AddPost(Post item);
        Task<int> AddPostAsync(Post item);
        int UpdatePost(Post item);
        Task<int> UpdatePostAsync(Post item);
        int DeletePost(int? id);
        Task<int> DeletePostAsync(int? id);
    }
}
