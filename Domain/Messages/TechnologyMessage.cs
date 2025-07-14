using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Messages
{
    public record TechnologyMessage(
        Guid Id,
        string Description
    );
}