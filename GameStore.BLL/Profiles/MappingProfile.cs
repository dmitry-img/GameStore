using AutoMapper;
using GameStore.BLL.DTOs;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Game, GameDTO>().ReverseMap();
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}
