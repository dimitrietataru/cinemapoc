﻿using AutoMapper;
using CMS.Controllers;
using CMS.Models.Cinema;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Testing.CinemaTest
{
    [Trait("Categoty", ". Cinema Controller")]
    public class CinemaControllerTest
    {
        private readonly ModelFaker modelFaker;
        private readonly Mock<ICinemaService> mockCinemaService;
        private readonly Mock<IMapper> mockMapper;
        private readonly CinemaController controllerUnderTest;

        public CinemaControllerTest()
        {
            modelFaker = new ModelFaker();
            mockCinemaService = new Mock<ICinemaService>();
            mockMapper = new Mock<IMapper>();
            controllerUnderTest = new CinemaController(
                mockCinemaService.Object,
                mockMapper.Object);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "CinemaController/Index -> cinemas")]
        internal async void GivenIndexCalledWhenCinemasExistThenReturnsCinemas()
        {
            // Arange
            var dbModels = modelFaker.GetTestCinemas(10);
            var viewModels = modelFaker.GetTestCinemaIndexes(10);
            mockCinemaService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(dbModels);
            mockMapper
                .Setup(_ => _.Map<List<CinemaIndexViewModel>>(dbModels))
                .Returns(viewModels);

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockCinemaService.Verify(_ => _.GetAllAsync(), Times.Once());
            mockMapper.Verify(_ => _.Map<List<CinemaIndexViewModel>>(dbModels), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is List<CinemaIndexViewModel>);
            Assert.Same(viewModels, result.Model);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "CinemaController/Index -> no cinemas")]
        internal async void GivenIndexCalledWhenNoCinemaExistsThenReturnsNoCinema()
        {
            // Arange
            var dbModels = modelFaker.GetTestCinemas(0);
            var viewModels = modelFaker.GetTestCinemaIndexes(0);
            mockCinemaService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(dbModels);
            mockMapper
                .Setup(_ => _.Map<List<CinemaIndexViewModel>>(dbModels))
                .Returns(viewModels);

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockCinemaService.Verify(_ => _.GetAllAsync(), Times.Once());
            mockMapper.Verify(_ => _.Map<List<CinemaIndexViewModel>>(dbModels), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is List<CinemaIndexViewModel>);
            Assert.Same(viewModels, result.Model);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "CinemaController/Index -> exception")]
        internal async void GivenIndexCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockCinemaService
                .Setup(_ => _.GetAllAsync())
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockCinemaService.Verify(_ => _.GetAllAsync(), Times.Once());
            mockMapper.Verify(_ => _.Map<List<CinemaIndexViewModel>>(It.IsAny<List<Cinema>>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Details")]
        [Fact(DisplayName = "CinemaController/Details -> cinema")]
        internal async void GivenDetailsCalledWhenCinemaExistsThenReturnsCinema()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaDetails();
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);
            mockMapper
                .Setup(_ => _.Map<CinemaDetailsViewModel>(dbModel))
                .Returns(viewModel);

            // Act
            var response = await controllerUnderTest.Details(default);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<CinemaDetailsViewModel>(dbModel), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is CinemaDetailsViewModel);
            Assert.Same(viewModel, result.Model);
        }

        [Trait("Category", "Details")]
        [Fact(DisplayName = "CinemaController/Details -> not found")]
        internal async void GivenDetailsCalledWhenCinemaNotFoundThenReturnsNotFound()
        {
            // Arange
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync((Cinema)null);

            // Act
            var response = await controllerUnderTest.Details(default);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<CinemaDetailsViewModel>(It.IsAny<Cinema>()), Times.Never());
            var result = Assert.IsType<NotFoundResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
        }

        [Trait("Category", "Details")]
        [Fact(DisplayName = "CinemaController/Details -> exception")]
        internal async void GivenDetailsCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Details(default);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<CinemaDetailsViewModel>(It.IsAny<Cinema>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }
    }
}
