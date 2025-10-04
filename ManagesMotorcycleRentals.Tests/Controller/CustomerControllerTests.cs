using FluentAssertions;
using ManagesMotorcycleRentals.API.Controllers;
using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ManagesMotorcycleRentals.Tests.Controller
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _serviceMock = new();
        private readonly Notify _notify = new();

        [Test]
        public async Task CreateCustomerAsync_ShouldReturnOk()
        {
            // Arrange
            var dto = new CreateCustomerDto();
            _serviceMock.Setup(x => x.CreateCustomerAsync(dto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            var controller = new CustomerController(_notify, _serviceMock.Object);

            // Act
            var result = await controller.CreateCustomerAsync(dto, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task CreateRentalAsync_ShouldReturnOk()
        {
            // Arrange
            var dto = new CreateCustomerRentalMotorcycleDto();
            _serviceMock.Setup(x => x.CreateCustomerRentalMotorcycleAsync(dto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            var controller = new CustomerController(_notify, _serviceMock.Object);

            // Act
            var result = await controller.CreateRentalAsync(dto, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}