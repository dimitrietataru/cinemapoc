using AutoMapper;
using CMS.Controllers;
using CMS.Models.Cinema;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
    }
}
