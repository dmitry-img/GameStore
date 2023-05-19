using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Pipelines;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Xunit;

namespace GameStore.BLL.UnitTests.Pipelines
{
    public class GameFilterOperationsTests : BaseTest
    {
        private readonly GameFilterOperations _gameFilterOperations;

        public GameFilterOperationsTests()
        {
            _gameFilterOperations = new GameFilterOperations();
        }

        [Fact]
        public void FilterByName_ShouldReturnCorrectGames()
        {
            var operation = _gameFilterOperations.CreateNameOperation("Warcraft");

            var result = ApplyOperation(operation);

            Assert.All(result, game => Assert.Contains("Warcraft", game.Name));
        }

        [Fact]
        public void FilterByGenre_ShouldReturnCorrectGames()
        {
            var operation = _gameFilterOperations.CreateGenreOperation(new List<int> { 1 });

            var result = ApplyOperation(operation);

            Assert.All(result, game => Assert.Contains(game.Genres, genre => genre.Id == 1));
        }

        [Fact]
        public void FilterByPlatform_ShouldReturnCorrectGames()
        {
            var operation = _gameFilterOperations.CreatePlatformOperation(new List<int> { 3 });

            var result = ApplyOperation(operation);

            Assert.All(result, game => Assert.Contains(game.PlatformTypes, platform => platform.Id == 3));
        }

        [Fact]
        public void FilterByPublisher_ShouldReturnCorrectGames()
        {
            var operation = _gameFilterOperations.CreatePublisherOperation(new List<int> { 2 });

            var result = ApplyOperation(operation);

            Assert.All(result, game => Assert.Equal(2, game.PublisherId));
        }

        [Fact]
        public void FilterByPrice_ShouldReturnCorrectGames()
        {
            var operation = _gameFilterOperations.CreatePriceOperation(30, 100);

            var result = ApplyOperation(operation);

            Assert.All(result, game => Assert.InRange(game.Price, 30, 100));
        }

        [Fact]
        public void FilterByDate_ShouldReturnCorrectGames()
        {
            var operation = _gameFilterOperations.CreateDateFilterOperation(DateFilterOption.LastMonth);

            var result = ApplyOperation(operation);

            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
            Assert.All(result, game => Assert.True(game.CreatedAt >= oneMonthAgo));
        }

        private List<Game> ApplyOperation(IOperation<IQueryable<Game>> operation)
        {
            return operation.Invoke(Games.AsQueryable()).ToList();
        }
    }
}
