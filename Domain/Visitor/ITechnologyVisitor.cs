using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Visitor
{
    public interface ITechnologyVisitor
    {
        Guid Id { get; set; }
        string Description { get; set; }
    }
}