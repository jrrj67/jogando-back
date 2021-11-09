using api.Controllers;
using api.Data.Requests;
using api.Data.Responses;
using api.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
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
            _mockService.Setup(service => service.GetAll()).Returns(new List<RolesResponse>()
            {
                new RolesResponse(),
                new RolesResponse(),
                new RolesResponse(),
            });

            var result = _controller.GetAll();

            var response = Assert.IsAssignableFrom<IActionResult>(result);

            var roles = Assert.IsType<List<RolesResponse>>(((OkObjectResult)response).Value);

            Assert.Equal(3, roles.Count);
        }

        [Fact]
        public void GetAll_ReturnsBadRequestIfExceptionIsThrown()
        {
            _mockService.Setup(service => service.GetAll()).Throws(new Exception("Test"));

            var result = _controller.GetAll();

            var response = Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}