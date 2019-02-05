using AutoMapper;
using CMS.Controllers;
using CMS.Models;
using CMS.Models.Cinema;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Testing.CinemaTests
{
    [Trait("Category", ". Cinema Controller")]
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
                .Setup(_ => _.GetPagedAsync(It.IsAny<IQueryable<Cinema>>(), 0, 10))
                .ReturnsAsync(dbModels);
            mockCinemaService
                .Setup(_ => _.GetCountAsync())
                .ReturnsAsync(10);
            mockMapper
                .Setup(_ => _.Map<List<CinemaIndexViewModel>>(dbModels))
                .Returns(viewModels);

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockCinemaService.Verify(_ => _.GetPagedAsync(It.IsAny<IQueryable<Cinema>>(), 0, 10), Times.Once());
            mockMapper.Verify(_ => _.Map<List<CinemaIndexViewModel>>(dbModels), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            var resultModel = Assert.IsType<PagedViewModel<CinemaIndexViewModel>>(result.Model);
            Assert.Same(viewModels, resultModel.Items);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "CinemaController/Index -> no cinemas")]
        internal async void GivenIndexCalledWhenNoCinemaExistsThenReturnsNoCinema()
        {
            // Arange
            var dbModels = modelFaker.GetTestCinemas(0);
            var viewModels = modelFaker.GetTestCinemaIndexes(0);
            mockCinemaService
                .Setup(_ => _.GetPagedAsync(It.IsAny<IQueryable<Cinema>>(), 0, 10))
                .ReturnsAsync(dbModels);
            mockCinemaService
                .Setup(_ => _.GetCountAsync())
                .ReturnsAsync(0);
            mockMapper
                .Setup(_ => _.Map<List<CinemaIndexViewModel>>(dbModels))
                .Returns(viewModels);

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockCinemaService.Verify(_ => _.GetPagedAsync(It.IsAny<IQueryable<Cinema>>(), 0, 10), Times.Once());
            mockMapper.Verify(_ => _.Map<List<CinemaIndexViewModel>>(dbModels), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            var resultModel = Assert.IsType<PagedViewModel<CinemaIndexViewModel>>(result.Model);
            Assert.Same(viewModels, resultModel.Items);
        }

        [Trait("Category", "Index")]
        [Fact(DisplayName = "CinemaController/Index -> exception")]
        internal async void GivenIndexCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockCinemaService
                .Setup(_ => _.GetPagedAsync(It.IsAny<IQueryable<Cinema>>(), 0, 10))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Index();

            // Assert
            mockCinemaService.Verify(_ => _.GetPagedAsync(It.IsAny<IQueryable<Cinema>>(), 0, 10), Times.Once());
            mockCinemaService.Verify(_ => _.GetCountAsync(), Times.Never());
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

        [Trait("Category", "Create")]
        [Fact(DisplayName = "CinemaController/Create/Get -> view")]
        internal async void GivenCreateViewCalledThenReturnsCreateView()
        {
            // Arange

            // Act
            var response = await controllerUnderTest.Create();

            // Assert
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "CinemaController/Create -> success")]
        internal async void GivenCreateCalledWhenInputIsValidThenCreatesCinema()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaCreate();
            mockMapper
                .Setup(_ => _.Map<Cinema>(viewModel))
                .Returns(dbModel);

            // Act
            var response = await controllerUnderTest.Create(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Cinema>(viewModel), Times.Once());
            mockCinemaService.Verify(_ => _.CreateAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "CinemaController/Create -> invalid input")]
        internal async void GivenCreateCalledWhenInputIsNotValidThenHandlesGracefully()
        {
            // Arange
            var viewModel = modelFaker.GetTestCinemaCreate();

            // Act
            controllerUnderTest.ModelState.AddModelError("key", "errorMessage");
            var response = await controllerUnderTest.Create(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Cinema>(viewModel), Times.Never());
            mockCinemaService.Verify(_ => _.CreateAsync(It.IsAny<Cinema>()), Times.Never());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is CinemaCreateViewModel);
        }

        [Trait("Category", "Create")]
        [Fact(DisplayName = "CinemaController/Create -> exception")]
        internal async void GivenCreateCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaCreate();
            mockMapper
                .Setup(_ => _.Map<Cinema>(viewModel))
                .Returns(dbModel);
            mockCinemaService
                .Setup(_ => _.CreateAsync(dbModel))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Create(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Cinema>(viewModel), Times.Once());
            mockCinemaService.Verify(_ => _.CreateAsync(dbModel), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is CinemaCreateViewModel);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "CinemaController/Edit/Get -> view")]
        internal async void GivenEditViewCalledWhenCinemaExistsThenReturnsEditView()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaEdit();
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);
            mockMapper
                .Setup(_ => _.Map<CinemaEditViewModel>(dbModel))
                .Returns(viewModel);

            // Act
            var response = await controllerUnderTest.Edit((Guid)default);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<CinemaEditViewModel>(dbModel), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is CinemaEditViewModel);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "CinemaController/Edit/Get -> exception")]
        internal async void GivenEditViewCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Edit((Guid)default);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<CinemaEditViewModel>(It.IsAny<Cinema>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "CinemaController/Edit -> success")]
        internal async void GivenEditCalledWhenInputIsValidThenEditsCinema()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaEdit();
            mockMapper
                .Setup(_ => _.Map<Cinema>(viewModel))
                .Returns(dbModel);

            // Act
            var response = await controllerUnderTest.Edit(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Cinema>(viewModel), Times.Once());
            mockCinemaService.Verify(_ => _.UpdateAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Details", result.ActionName);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "CinemaController/Edit -> invalid input")]
        internal async void GivenEditCalledWhenInputIsNotValidThenHandlesGracefully()
        {
            // Arange
            var viewModel = modelFaker.GetTestCinemaEdit();

            // Act
            controllerUnderTest.ModelState.AddModelError("key", "errorMessage");
            var response = await controllerUnderTest.Edit(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Cinema>(viewModel), Times.Never());
            mockCinemaService.Verify(_ => _.UpdateAsync(It.IsAny<Cinema>()), Times.Never());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is CinemaEditViewModel);
        }

        [Trait("Category", "Edit")]
        [Fact(DisplayName = "CinemaController/Edit -> exception")]
        internal async void GivenEditCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaEdit();
            mockMapper
                .Setup(_ => _.Map<Cinema>(viewModel))
                .Returns(dbModel);
            mockCinemaService
                .Setup(_ => _.UpdateAsync(dbModel))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Edit(viewModel);

            // Assert
            mockMapper.Verify(_ => _.Map<Cinema>(viewModel), Times.Once());
            mockCinemaService.Verify(_ => _.UpdateAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "CinemaController/Delete/Get -> view")]
        internal async void GivenDeleteViewCalledWhenCinemaExistsThenReturnsDeleteView()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaDelete();
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);
            mockMapper
                .Setup(_ => _.Map<CinemaDeleteViewModel>(dbModel))
                .Returns(viewModel);

            // Act
            var response = await controllerUnderTest.Delete((Guid)default);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<CinemaDeleteViewModel>(dbModel), Times.Once());
            var result = Assert.IsType<ViewResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.True(result.Model != null);
            Assert.True(result.Model is CinemaDeleteViewModel);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "CinemaController/Delete/Get -> exception")]
        internal async void GivenDeleteViewCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Delete((Guid)default);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockMapper.Verify(_ => _.Map<CinemaEditViewModel>(It.IsAny<Cinema>()), Times.Never());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "CinemaController/Delete -> success")]
        internal async void GivenDeleteCalledWhenCinemaExistsThenDeletesCinema()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaDelete();
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);

            // Act
            var response = await controllerUnderTest.Delete(viewModel);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockCinemaService.Verify(_ => _.DeleteAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
        }

        [Trait("Category", "Delete")]
        [Fact(DisplayName = "CinemaController/Delete -> exception")]
        internal async void GivenDeleteCalledWhenExceptionThrownThenHandleGracefully()
        {
            // Arange
            var dbModel = modelFaker.GetTestCinema();
            var viewModel = modelFaker.GetTestCinemaDelete();
            mockCinemaService
                .Setup(_ => _.GetByIdAsync(default))
                .ReturnsAsync(dbModel);
            mockCinemaService
                .Setup(_ => _.DeleteAsync(dbModel))
                .Throws<Exception>();

            // Act
            var response = await controllerUnderTest.Delete(viewModel);

            // Assert
            mockCinemaService.Verify(_ => _.GetByIdAsync(default), Times.Once());
            mockCinemaService.Verify(_ => _.DeleteAsync(dbModel), Times.Once());
            var result = Assert.IsType<RedirectToActionResult>(response);
            Assert.True(result != null);
            Assert.True(result is IActionResult);
            Assert.Equal("Index", result.ActionName);
            Assert.Equal("Home", result.ControllerName);
        }
    }
}
