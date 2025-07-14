using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public record TechnologyDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }

        public TechnologyDTO()
        {
        }

    }
}