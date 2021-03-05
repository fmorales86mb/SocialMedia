using Microsoft.EntityFrameworkCore;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository: IPostRepository
    {
        private readonly SocialMediaContext context;

        public PostRepository(SocialMediaContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await this.context.Posts.ToListAsync();
            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await this.context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return post;
        }

        // Metodo dummy
        //public async Task<IEnumerable<Post>> GetPosts()
        //{
        //    var posts = Enumerable.Range(1, 10).Select(x => new Post()
        //    {
        //        PostId = x,
        //        Description = $"Descripción {x}",
        //        Date = DateTime.Now,
        //        Image = $"https://misapis.com/{x}",
        //        UserId = x * 2
        //    });

        //    await Task.Delay(10);

        //    return posts;
        //}
    }
}
