using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.IRepository;
using Domain.Models;
using Domain.Interfaces;
using Infrastructure.DataModel;
using Microsoft.EntityFrameworkCore;
using Domain.Visitor;
namespace Infrastructure.Repositories;

public class TechnologyRepositoryEF : GenericRepositoryEF<ITechnology, Technology, TechnologyDataModel>, ITechnologyRepositoryEF
{
    private readonly IMapper _mapper;
    public TechnologyRepositoryEF(AbsanteeContext context, IMapper mapper) : base(context, mapper)
    {
        _mapper = mapper;
    }

    public async Task<Technology?> UpdateTechnology(ITechnology Technology)
    {
        var technologyDM = await _context.Set<TechnologyDataModel>()
            .FirstOrDefaultAsync(c => c.Id == Technology.Id);

        if (technologyDM == null) return null;

        _context.Set<TechnologyDataModel>().Update(technologyDM);
        await _context.SaveChangesAsync();
        return _mapper.Map<TechnologyDataModel, Technology>(technologyDM);
    }
    public async Task<bool> IsRepeated(string description)
    {
        return await this._context.Set<TechnologyDataModel>()
                .AnyAsync(c => c.Description == description);
    }

    public async Task<ITechnology?> GetByIdAsync(Guid id)
    {
        var technologyDM = await this._context.Set<TechnologyDataModel>()
                            .FirstOrDefaultAsync(c => c.Id == id);

        if (technologyDM == null)
            return null;

        var technology = _mapper.Map<TechnologyDataModel, Technology>(technologyDM);
        return technology;
    }
}