using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Models;
using Domain.Visitor;


namespace Infrastructure.DataModel
{
    public class TechnologyDataModel : ITechnologyVisitor
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public TechnologyDataModel()
        {
        }

        public TechnologyDataModel(Technology technology)
        {
            Id = technology.Id;
            Description = technology.Description;
        }
    }
}