﻿using AutoMapper;
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

        [Trait("Category", "Details")]
        [Fact(DisplayName = "MovieController/Details -> not found")]
        internal async void GivenDetailsCalledWhenMovieDontThenReturnsErrorPage()
        {
            // Arange
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync((Movie)null);

            // Act
            var response = await controllerUnderTest.Details(default);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<MovieDetailsViewModel>(It.IsAny<Movie>()), Times.Never());
            var result = Assert.IsType<NotFoundResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
        }

        [Trait("Category", "Details")]
        [Fact(DisplayName = "MovieController/Details -> exception")]
        internal async void GivenDetailsCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Details(default);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<MovieDetailsViewModel>(It.IsAny<Movie>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "MovieController/Create/Get -> view")]
        internal void GivenCreateViewCalledThenReturnsCreateView()
        {
            // Arange

            // Act
            var response = controllerUnderTest.Create();

            // Assert
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "MovieController/Create -> success")]
        internal async void GivenCreateCalledWhenInputIsValidThenCreatesMovie()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieCreate();
            mockMapper
                .Setup(_ => _.Map<Movie>(viewModel))
                .Returns(dbModel);

            // Act
            var response = await controllerUnderTest.Create(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Movie>(viewModel), Times.Once());
            mockMovieService.Verify(_ => _.CreateAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Details", result.ActionName);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "MovieController/Create -> invalid input")]
        internal async void GivenCreateCalledWhenInputIsNotValidThenHandlesGracefully()
        {
            // Arange
            var viewModel = modelFaker.GetTestMovieCreate();

            // Act
            controllerUnderTest.ModelState.AddModelError("key", "errorMessage");
            var response = await controllerUnderTest.Create(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Movie>(viewModel), Times.Never());
            mockMovieService.Verify(_ => _.CreateAsync(It.IsAny<Movie>()), Times.Never());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is MovieCreateViewModel);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "MovieController/Create -> exception")]
        internal async void GivenCreateCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieCreate();
            mockMapper
                .Setup(_ => _.Map<Movie>(viewModel))
                .Returns(dbModel);
            mockMovieService
                .Setup(_ => _.CreateAsync(dbModel))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Create(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Movie>(viewModel), Times.Once());
            mockMovieService.Verify(_ => _.CreateAsync(dbModel), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is MovieCreateViewModel);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "MovieController/Edit/Get -> view")]
        internal async void GivenEditViewCalledThenReturnsEditView()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieEdit();

            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);

            mockMapper
                .Setup(_ => _.Map<MovieEditViewModel>(dbModel))
                .Returns(viewModel);

            // Act
            var response = await controllerUnderTest.Edit((Guid)default);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once);
            mockMapper.Verify(_ => _.Map<MovieEditViewModel>(dbModel), Times.Once);
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result.Model != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model is MovieEditViewModel);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "MovieController/Edit/Get -> exception")]
        internal async void GivenEditViewCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Edit((Guid)default);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<MovieEditViewModel>(It.IsAny<Movie>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "MovieController/Edit -> success")]
        internal async void GivenEditCalledWhenInputIsValidThenEditsMovie()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieEdit();
            mockMapper
                .Setup(_ => _.Map<Movie>(viewModel))
                .Returns(dbModel);

            // Act
            var response = await controllerUnderTest.Edit(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Movie>(viewModel), Times.Once());
            mockMovieService.Verify(_ => _.UpdateAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Details", result.ActionName);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "MovieController/Edit -> invalid input")]
        internal async void GivenEditCalledWhenInputIsNotValidThenHandlesGracefully()
        {
            // Arange
            var viewModel = modelFaker.GetTestMovieEdit();

            // Act
            controllerUnderTest.ModelState.AddModelError("key", "errorMessage");
            var response = await controllerUnderTest.Edit(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Movie>(viewModel), Times.Never());
            mockMovieService.Verify(_ => _.UpdateAsync(It.IsAny<Movie>()), Times.Never());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is MovieEditViewModel);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "MovieController/Edit -> exception")]
        internal async void GivenEditCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieEdit();
            mockMapper
                .Setup(_ => _.Map<Movie>(viewModel))
                .Returns(dbModel);
            mockMovieService
                .Setup(_ => _.UpdateAsync(dbModel))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Edit(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Movie>(viewModel), Times.Once());
            mockMovieService.Verify(_ => _.UpdateAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "MovieController/Delete/Get -> view")]
        internal async void GivenDeleteViewCalledWhenMovieExistsThenReturnsDeleteView()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieDelete();
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);
            mockMapper
                .Setup(_ => _.Map<MovieDeleteViewModel>(dbModel))
                .Returns(viewModel);

            // Act
            var response = await controllerUnderTest.Delete((Guid)default);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<MovieDeleteViewModel>(dbModel), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is MovieDeleteViewModel);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "MovieController/Delete/Get -> exception")]
        internal async void GivenDeleteViewCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Delete((Guid)default);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<MovieEditViewModel>(It.IsAny<Movie>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "MovieController/Delete -> success")]
        internal async void GivenDeleteCalledWhenMovieExistsThenDeletesMovie()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieDelete();
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);

            // Act
            var response = await controllerUnderTest.Delete(viewModel);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMovieService.Verify(_ => _.DeleteAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "MovieController/Delete -> exception")]
        internal async void GivenDeleteCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            var dbModel = modelFaker.GetTestMovie();
            var viewModel = modelFaker.GetTestMovieDelete();
            mockMovieService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);
            mockMovieService
                .Setup(_ => _.DeleteAsync(dbModel))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Delete(viewModel);

            // Assert
            mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMovieService.Verify(_ => _.DeleteAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        //[Trait("Category", "CinemaMovie")]
        //[Fact(DisplayName = "MovieController/AssignMovie -> view")]
        //internal async void GivenAssignMovieViewCallWhenMovieExistsAndReturnsView()
        //{
        //    // Arange
        //    var dbModel = modelFaker.GetTestMovie();
        //    var viewModel = modelFaker.GetTestCinemaMovieViewModel();
        //    var cinemas = modelFaker.GetTestCinemas(10);

        //    mockMovieService
        //        .Setup(_ => _.GetByIdAsync(default))
        //        .ReturnsAsync(dbModel);

        //    mockCinemaService
        //        .Setup(_ => _.GetAllAsync())
        //        .ReturnsAsync(cinemas);

        //    // Act
        //    var response = await controllerUnderTest.AssignMovie((Guid)default);

        //    // Assert
        //    mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once);
        //    mockCinemaService.Verify(_ => _.GetAllAsync(), Times.Once);
        //    var result = Assert.IsType<RedirectToActionResult>(response);
        //    Assert.True(result != null);
        //    Assert.True(result is IActionResult);
        //}

        //[Trait("Category", "CinemaMovie")]
        //[Fact(DisplayName = "MovieController/AssignMovie -> exception")]
        //internal async void GivenAssignMovieViewCallWhenMovieDontExistAndReturnsException()
        //{
        //    // Arange
        //    var dbModel = modelFaker.GetTestMovie();
        //    var viewModel = modelFaker.GetTestCinemaMovieViewModel();
        //    var cinemas = modelFaker.GetTestCinemas(10);

        //    mockMovieService
        //        .Setup(_ => _.GetByIdAsync(default))
        //        .Throws<Exception>();

        //    mockCinemaService
        //        .Setup(_ => _.GetAllAsync())
        //        .ReturnsAsync(cinemas);

        //    // Act
        //    var response = await controllerUnderTest.AssignMovie((Guid)default);

        //    // Assert
        //    mockMovieService.Verify(_ => _.GetByIdAsync(default), Times.Once);
        //    mockCinemaService.Verify(_ => _.GetAllAsync(), Times.Never);
        //    var result = Assert.IsType<RedirectToActionResult>(response);
        //    Assert.True(result != null);
        //    Assert.True(result is IActionResult);
        //    Assert.Equal("Index", result.ActionName);
        //    Assert.Equal("Error", result.ControllerName);
        //}
    }
}
