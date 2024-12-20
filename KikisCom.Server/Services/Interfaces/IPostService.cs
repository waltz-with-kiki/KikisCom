using KikisCom.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace KikisCom.Server.Services.Interfaces
{
    public interface IPostService
    {
        public Task<(ObjectResult, List<Post>?)> GetPostsAsync();
    }
}
