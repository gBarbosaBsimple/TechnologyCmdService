using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.IRepository;
using Domain.Models;
using Domain.Visitor;

namespace Domain.Factory
{
    public class TechnologyFactory : ITechnologyFactory
    {
        private readonly ITechnologyRepositoryEF _TechnologyRepository;

        public TechnologyFactory(ITechnologyRepositoryEF TechnologyRepository)
        {
            _TechnologyRepository = TechnologyRepository;
        }

        public async Task<Technology> CreateAsync(string description)
        {
            //unicidade
            var existingTechnology = await _TechnologyRepository.IsRepeated(description);
            if (existingTechnology)
            {
                throw new ArgumentException("Já existe uma tecnologia .");
            }

            return new Technology(description);
        }

        public Technology Create(ITechnologyVisitor visitor)
        {
            return new Technology(visitor.Id, visitor.Description);
        }
    }
}