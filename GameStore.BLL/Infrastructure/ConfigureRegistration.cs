using System.IO;
using AutoMapper;
using FluentValidation;
using GameStore.BLL.DTOs.Auth;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.Order;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.DTOs.Role;
using GameStore.BLL.DTOs.ShoppingCart;
using GameStore.BLL.DTOs.User;
using GameStore.BLL.Enums;
using GameStore.BLL.Factories;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Profiles;
using GameStore.BLL.Services;
using GameStore.BLL.Strategies.Payment;
using GameStore.BLL.Strategies.Sorting;
using GameStore.BLL.Validators;
using GameStore.BLL.Validators.Comment;
using GameStore.BLL.Validators.Game;
using GameStore.BLL.Validators.Genre;
using GameStore.BLL.Validators.Order;
using GameStore.BLL.Validators.PlatformType;
using GameStore.BLL.Validators.Publisher;
using GameStore.BLL.Validators.Role;
using GameStore.BLL.Validators.ShoppingCart;
using GameStore.BLL.Validators.User;
using GameStore.DAL.Entities;
using Unity;

namespace GameStore.BLL.Infrastructure
{
    public static class ConfigureRegistration
    {
        public static IUnityContainer RegisterBLLTypes(this IUnityContainer container)
        {
            var mapperConfiguration = new MapperConfiguration(conf =>
            {
                conf.AddProfile<MappingProfile>();
            });

            container.RegisterInstance<IMapper>(mapperConfiguration.CreateMapper());

            container.RegisterType<IGameService, GameService>();
            container.RegisterType<ICommentService, CommentService>();
            container.RegisterType<IGenreService, GenreService>();
            container.RegisterType<IPlatformTypeService, PlatformTypeService>();
            container.RegisterType<IPublisherService, PublisherService>();
            container.RegisterType<IShoppingCartService, ShoppingCartService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IPaymentService, PaymentService>();
            container.RegisterType<IAuthService, AuthService>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IRoleService, RoleService>();

            container.RegisterType<IPaymentStrategyFactory, PaymentStrategyFactory>();
            container.RegisterType<IPaymentStrategy<MemoryStream>, BankPaymentStrategy>(PaymentType.Bank.ToString());
            container.RegisterType<IPaymentStrategy<int>, IBoxPaymentStrategy>(PaymentType.IBox.ToString());
            container.RegisterType<IPaymentStrategy<int>, VisaPaymentStrategy>(PaymentType.Visa.ToString());
            container.RegisterType<ISortStrategyFactory, SortStrategyFactory>();
            container.RegisterType<ISortStrategy<Game>, MostViewedSortStrategy>(SortOption.MostViewed.ToString());
            container.RegisterType<ISortStrategy<Game>, MostCommentedSortStrategy>(SortOption.MostCommented.ToString());
            container.RegisterType<ISortStrategy<Game>, PriceAscSortStrategy>(SortOption.PriceAscending.ToString());
            container.RegisterType<ISortStrategy<Game>, PriceDescSortStrategy>(SortOption.PriceDescending.ToString());
            container.RegisterType<ISortStrategy<Game>, NewSortStrategy>(SortOption.New.ToString());

            container.RegisterType<IValidator<CreateGameDTO>, CreateGameDTOValidator>();
            container.RegisterType<IValidator<UpdateGameDTO>, UpdateGameDTOValidator>();
            container.RegisterType<IValidator<FilterGameDTO>, FilterGameDTOValidator>();
            container.RegisterType<IValidator<CreateCommentDTO>, CreateCommentDTOValidator>();
            container.RegisterType<IValidator<CreatePublisherDTO>, CreatePublisherDTOValidator>();
            container.RegisterType<IValidator<UpdatePublisherDTO>, UpdatePublisherDTOValidator>();
            container.RegisterType<IValidator<RegistrationDTO>, RegistrationDTOValidator>();
            container.RegisterType<IValidator<CreateUserDTO>, CreateUserDTOValidator>();
            container.RegisterType<IValidator<UpdateUserDTO>, UpdateUserDTOValidator>();
            container.RegisterType<IValidator<UpdateOrderDTO>, UpdateOrderDTOValidator>();
            container.RegisterType<IValidator<UpdateOrderDetailDTO>, UpdateOrderDetailDTOValidator>();
            container.RegisterType<IValidator<CreateGenreDTO>, CreateGenreDTOValidator>();
            container.RegisterType<IValidator<UpdateGenreDTO>, UpdateGenreDTOValidator>();
            container.RegisterType<IValidator<CreatePlatformTypeDTO>, CreatePlatformTypeDTOValidator>();
            container.RegisterType<IValidator<UpdatePlatformTypeDTO>, UpdatePlatformTypeDTOValidator>();
            container.RegisterType<IValidator<CreateRoleDTO>, CreateRoleDTOValidator>();
            container.RegisterType<IValidator<UpdateRoleDTO>, UpdateRoleDTOValidator>();
            container.RegisterType<IValidator<CreateShoppingCartItemDTO>, CreateShoppingCartItemDTOValidator>();


            container.RegisterType<IValidationService, ValidationService>();

            return container;
        }
    }
}
