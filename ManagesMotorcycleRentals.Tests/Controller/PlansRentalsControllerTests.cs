using FluentAssertions;
using ManagesMotorcycleRentals.API.Controllers;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Shared;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ManagesMotorcycleRentals.Tests.Controller
{
    public class PlansRentalsControllerTests
    {
        private readonly Mock<IPlanRentalsService> _serviceMock = new();
        private readonly Notify _notify = new();

        [Test]
        public async Task GetPlansRentals_ShouldReturnOk()
        {
            _serviceMock.Setup(x => x.GetPlansRentalsAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new List<Application.DTOs.PlansRentalDto>()
                        {
                            new Application.DTOs.PlansRentalDto
                            {
                                Uid = Guid.NewGuid(),
                                Days = 7,                                
                                PricePerDay = 500.00m,
                                TotalPrice = 3500.00m
                            },
                            new Application.DTOs.PlansRentalDto
                            {
                                Uid = Guid.NewGuid(),
                                Days = 15,                                
                                PricePerDay = 450.00m,
                                TotalPrice = 6750.00m
                            },

                        });

            var controller = new PlansRentalsController(_notify, _serviceMock.Object);

            var result = await controller.CreateCustomerAsync(CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GetPlanRentalById_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            _serviceMock.Setup(x => x.GetPlansRentalsByUIdAsync(id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new Application.DTOs.PlansRentalDto()
                        {
                            Uid = id,
                            Days = 7,
                            PricePerDay = 500.00m,
                            TotalPrice = 3500.00m
                        });

            var controller = new PlansRentalsController(_notify, _serviceMock.Object);

            var result = await controller.CreateCustomerAsync(id, CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
