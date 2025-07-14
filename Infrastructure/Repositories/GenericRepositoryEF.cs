using AutoMapper;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class GenericRepositoryEF<TInterface, TDomain, TDataModel>
        where TInterface : class
        where TDomain : class, TInterface
        where TDataModel : class
    {
        protected readonly DbContext _context;
        private readonly IMapper _mapper;
        public GenericRepositoryEF(DbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TInterface> AddAsync(TInterface entity)
        {
            var domainEntity = (TDomain)entity;
            var dataModel = _mapper.Map<TDomain, TDataModel>(domainEntity);
            _context.Set<TDataModel>().Add(dataModel);
            await SaveChangesAsync();
            return _mapper.Map<TDataModel, TDomain>(dataModel);
        }
        public async Task AddRangeAsync(IEnumerable<TInterface> entities)
        {
            var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>((TDomain)e));
            _context.Set<TDataModel>().AddRange(dataModels);
            await SaveChangesAsync();
        }
        public async Task RemoveAsync(TInterface entity)
        {
            var dataModel = _mapper.Map<TDomain, TDataModel>((TDomain)entity);
            _context.Set<TDataModel>().Remove(dataModel);
            await SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TInterface> entities)
        {
            var dataModels = entities.Select(e => _mapper.Map<TDomain, TDataModel>((TDomain)e));
            _context.Set<TDataModel>().RemoveRange(dataModels);
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
