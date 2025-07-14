using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Domain.Messages;
using Application.DTO;
using Application.IServices;
using Application.IPublisher;
namespace InterfaceAdapters.Publisher
{
    public class MassTransitPublisher : IMessagePublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishTechnologyCreatedAsync(Guid Id, string description)
        {
            var eventMessage = new TechnologyMessage(Id, description);
            await _publishEndpoint.Publish(eventMessage,
             context =>
                {
                    context.Headers.Set("SenderId", InstanceInfo.InstanceId);
                });
            Console.WriteLine("TechnologyCreatedEvent published successfully");

        }
    }
}