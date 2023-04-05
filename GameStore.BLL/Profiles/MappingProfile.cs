using AutoMapper;
using GameStore.BLL.DTOs;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.DAL.Entities;
using System.Linq;

namespace GameStore.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGameDTO, Game>()
                .ForMember(dest => dest.Key, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.PlatformTypes, opt => opt.Ignore());


            CreateMap<UpdateGameDTO, Game>()
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.PlatformTypes, opt => opt.Ignore());

            CreateMap<Game, GetGameDTO>();

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, GetCommentDTO>();

            CreateMap<Genre, GetGenreDTO>();

            CreateMap<PlatformType, GetPlatformTypeDTO>();
        }
    }
}
