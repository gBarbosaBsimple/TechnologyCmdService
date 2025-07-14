using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.DTO;
using Application.IServices;
using Domain.Messages;
using MassTransit;
namespace InterfaceAdapters.Consumers
{
    public class TechnologyCreatedConsumer : IConsumer<TechnologyMessage>
    {
        private readonly ITechnologyService _technologyService;
        public TechnologyCreatedConsumer(ITechnologyService technologyService)
        {
            _technologyService = technologyService;
        }
        public async Task Consume(ConsumeContext<TechnologyMessage> context)
        {
            var senderId = context.Headers.Get<string>("SenderId");
            if (senderId == InstanceInfo.InstanceId)
                return;
            var msg = context.Message;
            await _technologyService.SubmitAsync(msg.Description);
        }
    }
}