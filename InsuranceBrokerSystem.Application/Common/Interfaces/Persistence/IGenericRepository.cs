
namespace InsuranceBrokerSystem.Application.Common.Interfaces.Persistence
{
    public interface IGenericRepository<TEntity>where TEntity : class
    {
        Task<List<TEntity>> GetAllEntitytiesAsync();
        Task<TEntity> GetEntityByIdAsync(int Id);
        Task<TEntity> GetEntityByIdWithIncludesAsync(int Id,params Expression<Func<TEntity, object>>[]includes);

        //Task<TEntity> GetUnitByNameAsync(string Name, CancellationToken cancellationToken);
        Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddEntityAsync(TEntity entity);
        Task<bool> UpdateEntityAsync(TEntity entity);
        Task<bool> DeleteEntityAsync(int Id);
    }
}
