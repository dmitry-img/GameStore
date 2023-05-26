using System;
using System.Collections.Generic;
using System.Linq;
using GameStore.BLL.Enums;
using GameStore.BLL.Interfaces;
using GameStore.BLL.Pipelines;
using GameStore.BLL.Pipelines.GameOperations;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Xunit;

namespace GameStore.BLL.UnitTests.Pipelines
{
    public class GameFilterOperationsTests : BaseTest
    {
        [Fact]
        public void FilterByName_ShouldReturnCorrectGames()
        {
            // Arrange
            var operation = new NameOperation("Warcraft");

            // Act
            var result = ApplyOperation(operation);

            // Assert
            Assert.All(result, game => Assert.Contains("Warcraft", game.Name));
        }

        [Fact]
        public void FilterByGenre_ShouldReturnCorrectGames()
        {
            // Arrange
            var operation = new GenreOperation(new List<int> { 1 });

            // Act
            var result = ApplyOperation(operation);

            // Assert
            Assert.All(result, game => Assert.Contains(game.Genres, genre => genre.Id == 1));
        }

        [Fact]
        public void FilterByPlatform_ShouldReturnCorrectGames()
        {
            // Arrange
            var operation = new PlatformOperation(new List<int> { 3 });

            // Act
            var result = ApplyOperation(operation);

            // Assert
            Assert.All(result, game => Assert.Contains(game.PlatformTypes, platform => platform.Id == 3));
        }

        [Fact]
        public void FilterByPublisher_ShouldReturnCorrectGames()
        {
            // Arrange
            var operation = new PublisherOperation(new List<int> { 2 });

            // Act
            var result = ApplyOperation(operation);

            // Assert
            Assert.All(result, game => Assert.Equal(2, game.PublisherId));
        }

        [Fact]
        public void FilterByPrice_ShouldReturnCorrectGames()
        {
            // Arrange
            var operation = new PriceOperation(30, 100);

            // Act
            var result = ApplyOperation(operation);

            // Assert
            Assert.All(result, game => Assert.InRange(game.Price, 30, 100));
        }

        [Fact]
        public void FilterByDate_ShouldReturnCorrectGames()
        {
            // Arrange
            var operation = new DateFilterOperation(DateFilterOption.LastMonth);

            // Act
            var result = ApplyOperation(operation);

            // Assert
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
            Assert.All(result, game => Assert.True(game.CreatedAt >= oneMonthAgo));
        }

        private List<Game> ApplyOperation(IOperation<IQueryable<Game>> operation)
        {
            return operation.Invoke(Games.AsQueryable()).ToList();
        }
    }
}
