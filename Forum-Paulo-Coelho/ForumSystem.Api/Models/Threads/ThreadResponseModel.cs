namespace ForumSystem.Api.Models.Threads
{
    using System;
    using AutoMapper;
    using ForumSystem.Api.Models.Contracts;
    using ForumSystem.Models;

    public class ThreadResponseModel : IMapFrom<Thread>, IHaveCustomMappings
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public string Creator { get; set; }

        public void CreateMappings(IConfiguration config)
        {
            config.CreateMap<Thread, ThreadResponseModel>()
                .ForMember(o => o.Creator, opt => opt.MapFrom(p => p.User.Nickname));
        }
    }
}