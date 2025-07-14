using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Factory;
using Domain.Models;
using Infrastructure.DataModel;

namespace Infrastructure.Resolvers
{
    public class TechnologyDataModelConverter : ITypeConverter<TechnologyDataModel, Technology>
    {
        private readonly ITechnologyFactory _technologyFactory;
        public TechnologyDataModelConverter(ITechnologyFactory technologyFactory)
        {
            _technologyFactory = technologyFactory;
        }
        public Technology Convert(TechnologyDataModel source, Technology destination, ResolutionContext context)
        {

            return _technologyFactory.Create(source);
        }
    }
}