using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PersonAPI_Second.Controllers;
using PersonAPI_Second.Domain;
using PersonAPI_Second.Features.Validators;
using Xunit;
using Assert = Xunit.Assert;

namespace PersonAPI_Second.Tests.Unit
{
    public class PersonControllerTests
    {
        private readonly Mock<IMediator> mockMediator;
        private readonly PersonController controller;

        public PersonControllerTests()
        {
            mockMediator = new Mock<IMediator>();

            // Injects the mock IMediator into the controller
            controller = new PersonController(mockMediator.Object);
        }

        [Fact]
        public async Task GetPersonById_ShouldReturnsOk_WhenPersonExists()
        {
            // ARRANGE
            var personId = Guid.NewGuid();
            var expectedPerson = new PersonDto(personId, "Test", "User", new DateOnly(1990, 1, 1), "123 Feet Street");

            mockMediator.Setup(m => m.Send(It.Is<GetPersonQuery>(q => q.Id == personId), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedPerson);

            // ACT
            var result = await controller.GetPersonById(personId);

            // ASSERT
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, okResult.StatusCode);

            // Verifies the content of the OkObjectResult
            var actualPerson = Assert.IsType<PersonDto>(okResult.Value);

            Assert.Equal(expectedPerson.Id, actualPerson.Id);
            Assert.Equal(expectedPerson.FirstName, actualPerson.FirstName);
        }

        [Fact]
        public async Task GetPersonById_ShouldThrowError_WhenPersonDoesNotExist()
        {
            // ARRANGE
            var personId = Guid.NewGuid();

            mockMediator.Setup(m => m.Send(It.Is<GetPersonQuery>(q => q.Id == personId), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PersonDto?) null );

            // ACT
            var result = await controller.GetPersonById(personId);

            // ASSERT
            Assert.IsType<NotFoundResult>(result);
        }
    }
}