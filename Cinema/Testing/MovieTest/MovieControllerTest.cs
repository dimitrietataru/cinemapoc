using AutoMapper;
using CMS.Controllers;
using CMS.Models.Movie;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

namespace Testing.MovieTest
{
    [Trait("Movie", ". Movie Controller")]
    public class MovieControllerTest
    {
        private readonly ModelFaker modelFaker;
        private readonly Mock<IMovieService> mockMovieService;
        private readonly Mock<IMapper> mockMapper;
        private readonly MovieController controllerUnderTest;

        public MovieControllerTest()
        {
            modelFaker = new ModelFaker();
            mockMovieService = new Mock<IMovieService>();
            mockMapper = new Mock<IMapper>();
            controllerUnderTest = new MovieController(
                mockMovieService.Object,
                mockMapper.Object);
        }

        [Trait("Movie", "Index")]
        [Fact(DisplayName = "MovieController/Index -> movies")]
        internal async void GivenIndexCalledWhenMoviesExistThenReturnsMovies()
        {
            // Arrage
            var dbModels = modelFaker.GetTestMovies(10);
            var viewModels = modelFaker.GetTestMovieIndex();
            mockMovieService
                .Setup(_ => _.GetPagedAsync(1, "Name", true, null))
                .ReturnsAsync(dbModels);
            mockMovieService
                .Setup(_ => _.GetCountAsync())
                .ReturnsAsync(10);

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockMovieService
                .Verify(_ => _.GetPagedAsync(1, "Name", true, null), Times.Once);
            mockMovieService
                .Verify(_ => _.GetCountAsync(), Times.Once);

            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            var resultModel = Assert.IsType<MovieIndexViewModel>(result.Model);
            Assert.Equal(viewModels.Count, resultModel.Count);
        }

        [Trait("Movie", "Index")]
        [Fact(DisplayName = "MovieController/Index -> exception")]
        internal async void GivenIndexCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockMovieService
               .Setup(_ => _.GetPagedAsync(0, "Name", true, null))
               .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockMovieService
                .Verify(_ => _.GetPagedAsync(0, "Name", true, null), Times.Never);
            mockMovieService
                .Verify(_ => _.GetCountAsync(), Times.Once);
            var resultModel = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(resultModel != null);
            Assert.True(resultModel is IActionResult);
            Assert.Equal("Index", resultModel.ActionName);
            Assert.Equal("Home", resultModel.ControllerName);
        }
    }
}
