using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using GameStore.Api.Controllers;
using GameStore.BLL.DTOs.Common;
using GameStore.BLL.DTOs.Genre;
using GameStore.BLL.Interfaces;
using Moq;
using Xunit;

namespace GameStore.Api.UnitTests.Controllers
{
    public class GenresControllerTests
    {
        private readonly GenresController _genresController;
        private readonly Mock<IGenreService> _genreServiceMock;
        private readonly Mock<IValidationService> _validationServiceMock;
        private readonly Mock<IValidator<CreateGenreDTO>> _createGenreValidatorMock;
        private readonly Mock<IValidator<UpdateGenreDTO>> _updateGenreValidatorMock;

        public GenresControllerTests()
        {
            _genreServiceMock = new Mock<IGenreService>();
            _validationServiceMock = new Mock<IValidationService>();
            _createGenreValidatorMock = new Mock<IValidator<CreateGenreDTO>>();
            _updateGenreValidatorMock = new Mock<IValidator<UpdateGenreDTO>>();

            _genresController = new GenresController(
                _genreServiceMock.Object,
                _validationServiceMock.Object,
                _createGenreValidatorMock.Object,
                _updateGenreValidatorMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldInvoke_CreateAsync()
        {
            // Arrange
            var genreDTO = new CreateGenreDTO
            {
                Name = "test-name"
            };

            // Act
            var result = await _genresController.Create(genreDTO);

            // Assert
            _genreServiceMock.Verify(x => x.CreateAsync(genreDTO), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldInvoke_UpdateAsync()
        {
            // Arrange
            var id = 1;

            var genreDTO = new UpdateGenreDTO
            {
                Name = "test-name"
            };

            // Act
            var result = await _genresController.Update(id, genreDTO);

            // Assert
            _genreServiceMock.Verify(x => x.UpdateAsync(id, genreDTO), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldInvoke_DeleteAsync()
        {
            // Arrange
            var id = 1;

            // Act
            var result = await _genresController.Delete(id);

            // Assert
            _genreServiceMock.Verify(x => x.DeleteAsync(id), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldInvoke_GetAllAsync()
        {
            // Act
            var result = await _genresController.GetAll();

            // Assert
            _genreServiceMock.Verify(x => x.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAllWithPagination_ShouldInvoke_GetAllWithPaginationAsync()
        {
            // Arrange
            var pagination = new PaginationDTO();

            // Act
            var result = await _genresController.GetAllWithPagination(pagination);

            // Assert
            _genreServiceMock.Verify(x => x.GetAllWithPaginationAsync(pagination), Times.Once);
        }
    }
}
