using AutoMapper;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.DAL.CacheEntities;
using GameStore.DAL.Entities;

namespace GameStore.BLL.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGameDTO, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Key, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.PlatformTypes, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Publisher, opt => opt.Ignore());

            CreateMap<UpdateGameDTO, Game>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Key, opt => opt.Ignore())
                .ForMember(dest => dest.Genres, opt => opt.Ignore())
                .ForMember(dest => dest.PlatformTypes, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Publisher, opt => opt.Ignore());

            CreateMap<Game, GetGameDTO>()
                .ForMember(dest => dest.PublisherCompanyName, opt =>
                    opt.MapFrom(src => src.Publisher.CompanyName));

            CreateMap<Game, GetGameBriefDTO>();

            CreateMap<CreateCommentDTO, Comment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.GameId, opt => opt.Ignore())
                .ForMember(dest => dest.Game, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.ParentComment, opt => opt.Ignore())
                .ForMember(dest => dest.ChildComments, opt => opt.Ignore());

            CreateMap<Comment, GetCommentDTO>();

            CreateMap<Genre, GetGenreDTO>();

            CreateMap<PlatformType, GetPlatformTypeDTO>();

            CreateMap<CreatePublisherDTO, Publisher>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Games, opt => opt.Ignore());

            CreateMap<Publisher, GetPublisherDTO>();

            CreateMap<Publisher, GetPublisherBriefDTO>();

            CreateMap<CreateShoppingCartItemDTO, ShoppingCartItem>();

            CreateMap<ShoppingCartItem, GetShoppingCartItemDTO>();

            CreateMap<ShoppingCartItem, OrderDetail>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.GamePrice));
        }
    }
}
