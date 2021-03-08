using AutoMapper;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;

namespace SocialMedia.Infrastructure.Mappings
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Post, PostDto>(); // Siempre y cuando sean iguales las propiedades, sino hay que configurar.
            CreateMap<PostDto, Post>();
        }
    }
}
