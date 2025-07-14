using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITechnology
    {
        Guid Id { get; set; }
        string Description { get; set; }
        public string ValidateDescription(string? description);
        void UpdateDescription(string description);
    }
}