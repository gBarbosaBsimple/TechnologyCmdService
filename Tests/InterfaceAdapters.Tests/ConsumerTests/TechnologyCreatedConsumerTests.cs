using Application.IServices;
using Domain.Messages;
using InterfaceAdapters.Consumers;
using MassTransit;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace InterfaceAdapters.Tests.ConsumerTests
{
    public class TechnologyCreatedConsumerTests
    {
        [Fact]
        public async Task Consume_ShouldCallSubmitAsync_WhenSenderIdIsDifferent()
        {
            // Arrange
            var technologyServiceMock = new Mock<ITechnologyService>();
            var consumer = new TechnologyCreatedConsumer(technologyServiceMock.Object);

            var message = new TechnologyMessage(Guid.NewGuid(), "New Tech");
            var contextMock = new Mock<ConsumeContext<TechnologyMessage>>();

            var headersMock = new Mock<Headers>();

            // Try this first:
            headersMock.Setup(h => h.Get<string>("SenderId", default(string)))
                       .Returns(Guid.NewGuid().ToString());

            // or, if no overload with default param, try:
            // headersMock.Setup(h => h.Get<string>("SenderId")).Returns(Guid.NewGuid().ToString());

            contextMock.Setup(c => c.Message).Returns(message);
            contextMock.Setup(c => c.Headers).Returns(headersMock.Object);

            // Act
            await consumer.Consume(contextMock.Object);

            // Assert
            technologyServiceMock.Verify(s => s.SubmitAsync(message.Description), Times.Once);
        }

        [Fact]
        public async Task Consume_ShouldNotCallSubmitAsync_WhenSenderIdIsSameAsInstanceId()
        {
            // Arrange
            var technologyServiceMock = new Mock<ITechnologyService>();
            var consumer = new TechnologyCreatedConsumer(technologyServiceMock.Object);

            var message = new TechnologyMessage(Guid.NewGuid(), "New Tech");
            var contextMock = new Mock<ConsumeContext<TechnologyMessage>>();

            var headersMock = new Mock<Headers>();

            // Setup Get<string> with one parameter only
            headersMock
                .Setup(h => h.Get<string>("SenderId", default))
                .Returns(InstanceInfo.InstanceId);

            contextMock.Setup(c => c.Message).Returns(message);
            contextMock.Setup(c => c.Headers).Returns(headersMock.Object);

            // Act
            await consumer.Consume(contextMock.Object);

            // Assert
            technologyServiceMock.Verify(s => s.SubmitAsync(It.IsAny<string>()), Times.Never);
        }


    }
}
