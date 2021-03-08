using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class PostController : ControllerBase
    {
        private readonly IPostRepository postRepository;
        private readonly IMapper mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            this.postRepository = postRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await this.postRepository.GetPosts();
            var postsDto = this.mapper.Map<IEnumerable<PostDto>>(posts); 

            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await this.postRepository.GetPost(id);
            var postDto = this.mapper.Map<PostDto>(post);

            return Ok(postDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = this.mapper.Map<Post>(postDto);
            await this.postRepository.InsertPost(post);

            return Ok(postDto);
        }
    }
}
