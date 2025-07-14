namespace Domain.IRepository;

using System.Linq.Expressions;

public interface IGenericRepositoryEF<TInterface, TDomain, TDataModel>
        where TInterface : class
        where TDomain : class, TInterface
        where TDataModel : class
{
    Task<TInterface?> GetByIdAsync(Guid id);
    Task<TInterface> AddAsync(TInterface entity);
    Task AddRangeAsync(IEnumerable<TInterface> entities);
    Task<int> SaveChangesAsync();
    Task RemoveAsync(TInterface entity);
    Task RemoveRangeAsync(IEnumerable<TInterface> entities);

}
