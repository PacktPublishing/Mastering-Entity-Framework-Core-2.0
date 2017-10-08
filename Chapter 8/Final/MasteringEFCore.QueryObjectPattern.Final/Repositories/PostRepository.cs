using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MasteringEFCore.QueryObjectPattern.Final.Models;
using MasteringEFCore.QueryObjectPattern.Final.Data;
using Microsoft.EntityFrameworkCore;

namespace MasteringEFCore.QueryObjectPattern.Final.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly BlogContext _context;

        public PostRepository(BlogContext context)
        {
            _context = context;
        }
        public IEnumerable<Post> FindPost(string keyword, int pageNumber, int pageCount)
        {
            return _context.Posts
                .Where(x => 
                    x.Title.ToLower().Contains(keyword.ToLower())
                    || x.Blog.Title.ToLower().Contains(keyword.ToLower())
                    || x.Blog.Subtitle.ToLower().Contains(keyword.ToLower())
                    || x.Category.Name.ToLower().Contains(keyword.ToLower())
                    || x.Content.ToLower().Contains(keyword.ToLower())
                    || x.Summary.ToLower().Contains(keyword.ToLower())
                    || x.Author.Username.ToLower().Contains(keyword.ToLower())
                    || x.Url.ToLower().Contains(keyword.ToLower()))
                .Skip(pageNumber-1).Take(pageCount);
        }

        public IEnumerable<Post> FindPostByAuthor(string author)
        {
            return _context.Posts
                .Where(x => x.Author.Username.ToLower().Contains(author.ToLower()));
        }

        public IEnumerable<Post> FindPostByCategory(string category)
        {
            return _context.Posts
                .Where(x => x.Category.Name.ToLower().Contains(category.ToLower()));
        }

        public IEnumerable<Post> FindPostByHighestVisitors()
        {
            return _context.Posts
                .OrderByDescending(x => x.VisitorCount);
        }

        public IEnumerable<Post> FindPostByPublishedYear(int year)
        {
            return _context.Posts
                .Where(x => x.PublishedDateTime.Year.Equals(year));
        }

        public IEnumerable<Post> FindPostByTitle(string title)
        {
            return _context.Posts
                .Where(x => x.Title.ToLower().Contains(title.ToLower()));
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category);
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.Include(p => p.Author).Include(p => p.Blog).Include(p => p.Category).ToListAsync();
        }

        public Post GetPostById(int? id, bool includeData = true)
        {
            return includeData 
                    ? _context.Posts.Include(p => p.Author)
                .Include(p => p.Blog)
                .Include(p => p.Category).SingleOrDefault(x => x.Id.Equals(id))
                    : _context.Posts.SingleOrDefault(x => x.Id.Equals(id));
        }

        public async Task<Post> GetPostByIdAsync(int? id, bool includeData = true)
        {
            return includeData
                    ? await _context.Posts.Include(p => p.Author)
                .Include(p => p.Blog)
                .Include(p => p.Category).SingleOrDefaultAsync(x => x.Id.Equals(id))
                    : await _context.Posts.SingleOrDefaultAsync(x => x.Id.Equals(id));
        }

        public int AddPost(Post item)
        {
            _context.Add(item);
            return _context.SaveChanges();
        }

        public async Task<int> AddPostAsync(Post item)
        {
            _context.Add(item);
            return await _context.SaveChangesAsync();
        }

        public int UpdatePost(Post item)
        {
            _context.Update(item);
            return _context.SaveChanges();
        }

        public async Task<int> UpdatePostAsync(Post item)
        {
            _context.Update(item);
            return await _context.SaveChangesAsync();
        }

        public int DeletePost(int? id)
        {
            var post = _context.Posts.SingleOrDefault(m => m.Id == id);
            _context.Posts.Remove(post);
            return _context.SaveChanges();
        }

        public async Task<int> DeletePostAsync(int? id)
        {
            var post = await _context.Posts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Posts.Remove(post);
            return await _context.SaveChangesAsync();
        }
    }
}
