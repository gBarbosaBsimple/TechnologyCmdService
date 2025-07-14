using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public interface ITechnologyFactory
    {
        Task<Technology> CreateAsync(string description);
        Technology Create(ITechnologyVisitor visitor);
    }
}