using System.Configuration;
using System.IO;
using AutoMapper;
using FluentValidation;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.DTOs.Publisher;
using GameStore.BLL.Enums;
using GameStore.BLL.Factories;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Pipelines;
using GameStore.BLL.Profiles;
using GameStore.BLL.Services;
using GameStore.BLL.Strategies;
using GameStore.BLL.Validators;
using StackExchange.Redis;
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

            container.RegisterType<IPaymentStrategyFactory, PaymentStrategyFactory>();
            container.RegisterType<IPaymentStrategy<MemoryStream>, BankPaymentStrategy>(PaymentType.Bank.ToString());
            container.RegisterType<IPaymentStrategy<int>, IBoxPaymentStrategy>(PaymentType.IBox.ToString());
            container.RegisterType<IPaymentStrategy<int>, VisaPaymentStrategy>(PaymentType.Visa.ToString());

            container.RegisterType<IGameFilterOperations, GameFilterOperations>();

            container.RegisterType<IValidator<CreateGameDTO>, CreateGameDTOValidator>();
            container.RegisterType<IValidator<UpdateGameDTO>, UpdateGameDTOValidator>();
            container.RegisterType<IValidator<CreateCommentDTO>, CreateCommentDTOValidator>();
            container.RegisterType<IValidator<CreatePublisherDTO>, CreatePublisherDTOValidator>();

            return container;
        }
    }
}
