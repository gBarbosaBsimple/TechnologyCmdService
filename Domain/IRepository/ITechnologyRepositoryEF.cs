using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Visitor;
using Domain.Models;
using Domain.Interfaces;
using Domain.IRepository;

namespace Domain.IRepository
{
    public interface ITechnologyRepositoryEF : IGenericRepositoryEF<ITechnology, Technology, ITechnologyVisitor>
    {
        Task<Technology?> UpdateTechnology(ITechnology Technology);
        Task<bool> IsRepeated(string ddescription);
        Task<ITechnology?> GetByIdAsync(Guid id);
    }
}