using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GameStore.BLL.DTOs.Comment;
using GameStore.BLL.DTOs.Game;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.DTOs.PlatformType;
using GameStore.BLL.Profiles;
using GameStore.DAL.Entities;
using Xunit;

namespace GameStore.BLL.UnitTests.Profiles
{
    public class MappingProfileTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            _configuration = new MapperConfiguration(config =>
                config.AddProfile<MappingProfile>());

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(CreateGameDTO), typeof(Game))]
        [InlineData(typeof(UpdateGameDTO), typeof(Game))]
        [InlineData(typeof(Game), typeof(GetGameDTO))]
        [InlineData(typeof(CreateCommentDTO), typeof(Comment))]
        [InlineData(typeof(Comment), typeof(GetCommentDTO))]
        [InlineData(typeof(Genre), typeof(GetGenreDTO))]
        [InlineData(typeof(PlatformType), typeof(GetPlatformTypeDTO))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
