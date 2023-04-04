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
                .ForMember(dest => dest.GameGenres, opt =>
                    opt.MapFrom(src =>
                        src.GenreIds.Select(gid =>
                            new GameGenre { GenreId = gid })))
                .ForMember(dest => dest.GamePlatformTypes, opt =>
                    opt.MapFrom(src =>
                        src.PlatformTypeIds.Select(ptid =>
                            new GamePlatformType { PlatformTypeId = ptid })));

            CreateMap<UpdateGameDTO, Game>()
                .ForMember(dest => dest.GameGenres, opt =>
                    opt.MapFrom(src =>
                        src.GenreIds.Select(gid =>
                            new GameGenre { GenreId = gid })))
                .ForMember(dest => dest.GamePlatformTypes, opt =>
                    opt.MapFrom(src =>
                        src.PlatformTypeIds.Select(ptid =>
                            new GamePlatformType { PlatformTypeId = ptid })));

            CreateMap<Game, GetGameDTO>()
                .ForMember(dest => dest.Genres, opt => 
                    opt.MapFrom(src => 
                        src.GameGenres.Select(gg => 
                            gg.Genre)))
                .ForMember(dest => dest.PlatformTypes, opt =>
                        opt.MapFrom(src =>
                            src.GamePlatformTypes.Select(gg =>
                                gg.PlatformType)));

            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<Comment, GetCommentDTO>();

            CreateMap<Genre, GetGenreDTO>();

            CreateMap<PlatformType, GetPlatformTypeDTO>();
        }
    }
}
