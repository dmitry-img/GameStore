using System.Linq;
using GameStore.BLL.Strategies.Sorting;
using GameStore.BLL.UnitTests.Common;
using Xunit;

namespace GameStore.BLL.UnitTests.Strategies
{
    public class SortingStrategyTests : BaseTest
    {
        [Fact]
        public void MostCommentedSortStrategy_ShouldSortByCommentsCount()
        {
            // Arrange
            var strategy = new MostCommentedSortStrategy();

            // Act
            var sortedGames = strategy.Sort(Games.AsQueryable());

            // Assert
            Assert.Equal(Games.OrderByDescending(game => game.Comments.Count).ToList(), sortedGames.ToList());
        }

        [Fact]
        public void MostViewedSortStrategy_ShouldSortByViews()
        {
            // Arrange
            var strategy = new MostViewedSortStrategy();

            // Act
            var sortedGames = strategy.Sort(Games.AsQueryable());

            // Assert
            Assert.Equal(Games.OrderByDescending(game => game.Views).ToList(), sortedGames.ToList());
        }

        [Fact]
        public void NewSortStrategy_ShouldSortByDeletedAt()
        {
            // Arrange
            var strategy = new NewSortStrategy();

            // Act
            var sortedGames = strategy.Sort(Games.AsQueryable());

            // Assert
            Assert.Equal(Games.OrderByDescending(game => game.DeletedAt).ToList(), sortedGames.ToList());
        }

        [Fact]
        public void PriceAscSortStrategy_ShouldSortByPriceAscending()
        {
            // Arrange
            var strategy = new PriceAscSortStrategy();

            // Act
            var sortedGames = strategy.Sort(Games.AsQueryable());

            // Assert
            Assert.Equal(Games.OrderBy(game => game.Price).ToList(), sortedGames.ToList());
        }

        [Fact]
        public void PriceDescSortStrategy_ShouldSortByPriceDescending()
        {
            // Arrange
            var strategy = new PriceDescSortStrategy();

            // Act
            var sortedGames = strategy.Sort(Games.AsQueryable());

            // Assert
            Assert.Equal(Games.OrderByDescending(game => game.Price).ToList(), sortedGames.ToList());
        }
    }
}
