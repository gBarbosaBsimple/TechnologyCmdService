using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.IPublisher
{
    public interface IMessagePublisher
    {
        Task PublishTechnologyCreatedAsync(Guid Id, string description);
    }
}