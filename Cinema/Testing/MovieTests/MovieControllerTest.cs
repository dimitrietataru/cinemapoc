using AutoMapper;
using CMS.Controllers;
using CMS.Models;
using CMS.Models.Movie;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Testing.MovieTests
{
    [Trait("Category", ". Movie Controller")]
    public class MovieControllerTest
    {
        private readonly ModelFaker modelFaker;
        private readonly Mock<IMovieService> mockMovieService;
        private readonly Mock<ICinemaService> mockCinemaService;
        private readonly Mock<ICinemaMovieService> mockCinemaMovieService;
        private readonly Mock<IMapper> mockMapper;
        private readonly MovieController controllerUnderTest;

        public MovieControllerTest()
        {
            modelFaker = new ModelFaker();
            mockMovieService = new Mock<IMovieService>();
            mockCinemaService = new Mock<ICinemaService>();
            mockCinemaMovieService = new Mock<ICinemaMovieService>();
            mockMapper = new Mock<IMapper>();
            controllerUnderTest = new MovieController(
                mockMovieService.Object,
                mockCinemaService.Object,
                mockCinemaMovieService.Object,
                mockMapper.Object);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "MovieController/Index -> movies")]
        internal async void GivenIndexCalledWhenMoviesExistThenReturnsMovies()
        {
            // Arange
            var dbModels = modelFaker.GetTestMovies(10);
            var viewModels = modelFaker.GetTestMovieIndexes(10);
            mockMovieService
                .Setup(_ => _.GetPagedAsync(It.IsAny<IQueryable<Movie>>(), 0, 10))
                .ReturnsAsync(dbModels);
            mockMovieService
                .Setup(_ => _.GetCountAsync())
                .ReturnsAsync(10);
            mockMapper
                .Setup(_ => _.Map<List<MovieIndexViewModel>>(dbModels))
                .Returns(viewModels);

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockMovieService.Verify(_ => _.GetPagedAsync(It.IsAny<IQueryable<Movie>>(), 0, 10), Times.Once());
            mockMapper.Verify(_ => _.Map<List<MovieIndexViewModel>>(dbModels), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            var resultModel = Assert.IsType<PagedViewModel<MovieIndexViewModel>>(result.Model);
            Assert.Same(viewModels, resultModel.Items);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "MovieController/Index -> no movies")]
        internal async void GivenIndexCalledWhenNoMovieExistsThenReturnsNoMovie()
        {
            // Arange
            var dbModels = modelFaker.GetTestMovies(0);
            var viewModels = modelFaker.GetTestMovieIndexes(0);
            mockMovieService
                .Setup(_ => _.GetPagedAsync(It.IsAny<IQueryable<Movie>>(), 0, 10))
                .ReturnsAsync(dbModels);
            mockMovieService
                .Setup(_ => _.GetCountAsync())
                .ReturnsAsync(0);
            mockMapper
                .Setup(_ => _.Map<List<MovieIndexViewModel>>(dbModels))
                .Returns(viewModels);

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockMovieService.Verify(_ => _.GetPagedAsync(It.IsAny<IQueryable<Movie>>(), 0, 10), Times.Once());
            mockMapper.Verify(_ => _.Map<List<MovieIndexViewModel>>(dbModels), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            var resultModel = Assert.IsType<PagedViewModel<MovieIndexViewModel>>(result.Model);
            Assert.Same(viewModels, resultModel.Items);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "MovieController/Index -> exception")]
        internal async void GivenIndexCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockMovieService
                .Setup(_ => _.GetPagedAsync(It.IsAny<IQueryable<Movie>>(), 0, 10))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockMovieService.Verify(_ => _.GetPagedAsync(It.IsAny<IQueryable<Movie>>(), 0, 10), Times.Once());
            mockMovieService.Verify(_ => _.GetCountAsync(), Times.Never());
            mockMapper.Verify(_ => _.Map<List<MovieIndexViewModel>>(It.IsAny<List<Movie>>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Details")]
        [Fact(DisplayName = "MovieController/Details -> movie")]
        internal async void GivenDetailsCalledWhenMovieExistsThenReturnsMovie()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieDetails();
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);
            mockMapper
                .Setup(_ => _.Map<MovieDetailsViewModel>(dbModel))
                .Returns(viewModel);

            // Act
            var response = await controllerUnderTest.Details(default);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<MovieDetailsViewModel>(dbModel), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is MovieDetailsViewModel);
            Assert.Same(viewModel, result.Model);
        }
    }
}
