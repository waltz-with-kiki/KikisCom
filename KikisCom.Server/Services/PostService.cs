using KikisCom.DAL.Data;
using KikisCom.Domain.Models;
using KikisCom.Server.Services.Interfaces;
using KikisCom.Server.WorkClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KikisCom.Server.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _dbContext;
        public PostService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<(ObjectResult, List<Post>?)> GetPostsAsync()
        {
            try
            { 
                var posts = await _dbContext.Posts.AsNoTracking().ToListAsync();
                return (new ObjectResult("") { StatusCode = 200 }, posts);
            }
            catch (Exception ex)
            {
                WriteLog.Error($"GetPosts method error: {ex}");
                return (new ObjectResult("Fatal server error") { StatusCode = 500 }, null);
            }
        }
    }
}
