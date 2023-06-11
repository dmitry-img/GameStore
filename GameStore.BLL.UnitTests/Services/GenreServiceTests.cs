using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.Services;
using GameStore.BLL.UnitTests.Common;
using GameStore.DAL.Entities;
using Moq;
using Xunit;

namespace GameStore.BLL.UnitTests.Services
{
    public class GenreServiceTests : BaseTest
    {
        private readonly GenreService _genreService;

        public GenreServiceTests()
        {
            _genreService = new GenreService(MockUow.Object, Mapper, MockLogger.Object);
        }

        [Fact]
        public async Task CreateAsync_CreatesGenre()
        {
            // Arrange
            var genreDto = new CreateGenreDTO { Name = "New" };

            // Act
            await _genreService.CreateAsync(genreDto);

            // Assert
            MockUow.Verify(u => u.Genres.Create(It.IsAny<Genre>()), Times.Once);
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAll_ReturnsAllGenres()
        {
            // Act
            var result = await _genreService.GetAllAsync();

            // Assert
            Assert.Equal(Genres.Count, result.Count());
        }

        [Fact]
        public async Task DeleteAsync_DeletesGenre()
        {
            // Arrange
            var id = 1;

            // Act
            await _genreService.DeleteAsync(id);

            // Assert
            MockUow.Verify(u => u.Genres.Delete(id), Times.Once);
            MockUow.Verify(u => u.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllWithPaginationAsync_ReturnsPaginatedGenres()
        {
            // Arrange
            var paginationDTO = new PaginationDTO { PageNumber = 1, PageSize = 2 };

            // Act
            var result = await _genreService.GetAllWithPaginationAsync(paginationDTO);

            // Assert
            Assert.Equal(2, result.Items.Count());
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateGenreCorrectlyBasedOnDTOAndSaveChanges()
        {
            // Arrange
            var genreId = 1;
            var oldParentGenreId = 2;
            var newParentGenreId = 3;

            var oldGenre = new Genre { Id = genreId, ParentGenreId = oldParentGenreId };
            var updatedGenreDto = new UpdateGenreDTO { ParentGenreId = newParentGenreId };

            MockUow.Setup(uow => uow.Genres.GetAsync(genreId)).ReturnsAsync(oldGenre);

            // Act
            await _genreService.UpdateAsync(genreId, updatedGenreDto);

            // Assert
            Assert.Equal(newParentGenreId, oldGenre.ParentGenreId);
            MockUow.Verify(uow => uow.SaveAsync(), Times.Once);
            MockLogger.Verify(logger => logger.Info($"Genre({genreId}) has been updated!"), Times.Once);
        }
    }
}
