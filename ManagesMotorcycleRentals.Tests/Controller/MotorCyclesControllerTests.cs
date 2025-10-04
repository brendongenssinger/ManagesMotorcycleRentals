using FluentAssertions;
using ManagesMotorcycleRentals.API.Controllers;
using ManagesMotorcycleRentals.Application.DTOs;
using ManagesMotorcycleRentals.Application.Services.Interfaces;
using ManagesMotorcycleRentals.Domain.Shared;
using ManagesMotorcycleRentals.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ManagesMotorcycleRentals.Tests.Controller
{
    public class MotorCyclesControllerTests
    {
        private readonly Mock<IMotorcyclesServices> _serviceMock = new();
        private readonly Notify _notify = new();

        [Test]
        public async Task CreateMotorCycle_ShouldReturnOk()
        {
            var dto = new MotorCycleCreateDto();
            _serviceMock.Setup(x => x.CreateMotorcycleAsync(dto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            var controller = new MotorCyclesController(_notify, _serviceMock.Object);

            var result = await controller.CreateMotorCycle(dto, CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task GetMotorCycleByLicensePlate_ShouldReturnOk()
        {
            _serviceMock.Setup(x => x.GetMotorcyles("ABC1234", It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new List<MotorCycleDtoResponse>()
                        {
                            new MotorCycleDtoResponse
                            {
                                Uid = Guid.NewGuid(),
                                Model = "Model X",                                
                                Year = 2022,
                                LicensePlate = "ABC1234",                                
                            }
                        });

            var controller = new MotorCyclesController(_notify, _serviceMock.Object);

            var result = await controller.GetMotorCycleByLicensePlate("ABC1234", CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task UpdateMotorCycle_ShouldReturnOk()
        {
            var dto = new MotorCycleUpdateDto();
            _serviceMock.Setup(x => x.UpdateMotorcycle(dto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            var controller = new MotorCyclesController(_notify, _serviceMock.Object);

            var result = await controller.UpdateMotorCycleByLicensePlate(dto, CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>();
        }

        [Test]
        public async Task DeleteMotorCycle_ShouldReturnOk()
        {
            var id = Guid.NewGuid();
            _serviceMock.Setup(x => x.DeleteMotorcycleByUId(id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(true);

            var controller = new MotorCyclesController(_notify, _serviceMock.Object);

            var result = await controller.DeleteMotorCycleByUId(id, CancellationToken.None);

            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
