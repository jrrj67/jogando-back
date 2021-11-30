using JogandoBack.API.Controllers;
using JogandoBack.API.Data.Requests;
using JogandoBack.API.Data.Responses;
using JogandoBack.API.Data.Services;
using JogandoBack.API.Data.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using tests.Utils;
using Xunit;

namespace tests
{
    public class RolesControllerTests
    {
        private readonly Mock<ILogger<RolesController>> _logger;
        private readonly Mock<IBaseService<RolesResponse, RolesRequest>> _mockService;
        private readonly RolesController _controller;

        public RolesControllerTests()
        {
            _logger = new Mock<ILogger<RolesController>>();
            _mockService = new Mock<IBaseService<RolesResponse, RolesRequest>>();
            _controller = new RolesController(_logger.Object, _mockService.Object);
        }

        [Fact]
        public void GetAll_ReturnsExactNumberOfRoles()
        {
            // Setup

            _mockService.Setup(service => service.GetAll()).Returns(new List<RolesResponse>()
            {
                new RolesResponse(),
                new RolesResponse(),
                new RolesResponse(),
            });

            var result = _controller.GetAll();

            // Testing response return

            var response = Assert.IsType<OkObjectResult>(result);

            // Testing if there are a list of RolesResponse

            var roles = Assert.IsType<List<RolesResponse>>(response.Value);

            // Testing if the number of entries returned by service is the same of the response

            Assert.Equal(3, roles.Count);
        }

        [Fact]
        public void GetAll_ReturnsBadRequestIfExceptionIsThrown()
        {
            // Setup

            _mockService.Setup(service => service.GetAll()).Throws(new Exception("Test"));

            var result = _controller.GetAll();

            // Testing response return

            var response = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_ReturnsSingleEntryAndIdIsTheSame(int id)
        {
            // Setup

            _mockService.Setup(service => service.GetById(id)).Returns(new RolesResponse()
            {
                Id = id,
                Name = "Test"
            });

            var result = _controller.GetById(id);

            // Testing response return

            var response = Assert.IsType<OkObjectResult>(result);

            // Testing if there is only one entry

            var role = Assert.IsType<RolesResponse>(response.Value);

            // Testing if provided id is the same of the returned entry

            Assert.Equal(id, role.Id);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_ReturnsNotFoundIfArgumentExceptionIsThrown(int id)
        {
            // Setup

            _mockService.Setup(service => service.GetById(id)).Throws(new ArgumentException("Test"));

            var result = _controller.GetById(id);

            // Testing response return

            var response = Assert.IsType<NotFoundObjectResult>(result);
        }

        [Theory]
        [InlineData(1)]
        public void GetById_ReturnsBadRequestIfExceptionIsThrown(int id)
        {
            // Setup

            _mockService.Setup(service => service.GetById(id)).Throws(new Exception("Test"));

            var result = _controller.GetById(id);

            // Testing response return

            var response = Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void SaveAsync_ReturnsCreatedResponseWithLocation()
        {
            // Setup

            var roleId = 1;

            var request = new RolesRequest();

            _mockService.Setup(service => service.SaveAsync(request)).ReturnsAsync(new RolesResponse() { Id = roleId, Name = "Test" });

            _controller.ControllerContext.HttpContext = MockObjects.GetMockHttpContext(MockHttpContextConfig.Scheme, MockHttpContextConfig.Host,
                MockHttpContextConfig.Path);

            var uri = _controller.ControllerContext.HttpContext.Request.GetAbsoluteUri();

            var result = await _controller.SaveAsync(request);

            // Testing response return

            var response = Assert.IsType<CreatedResult>(result);

            // Testing location header

            Assert.Equal($"{uri}/{roleId}", response.Location);
        }

        [Fact]
        public async void SaveAsync_ReturnsBadRequestIfExceptionIsThrown()
        {
            // Setup

            var request = new RolesRequest();

            _mockService.Setup(service => service.SaveAsync(request)).Throws(new Exception("Test"));

            var result = await _controller.SaveAsync(request);

            // Testing response return

            var response = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}