
namespace InsuranceBrokerSystem.Infrastructure.Repositories
{
    public class GenericRepository<TEntity>:IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _appDbContext;

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<TEntity> AddEntityAsync(TEntity entity)
        {
            await  _appDbContext.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> DeleteEntityAsync(int Id)
        {
            var entry = await _appDbContext.Set<TEntity>().Where(i => i.Id == Id).ExecuteUpdateAsync(c => c.SetProperty(i => i.IsDeleted, true));
            if (entry != null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<TEntity>> GetAllEntitytiesAsync()
        {
            return await _appDbContext.Set<TEntity>().Where(i=>i.IsDeleted == false).ToListAsync();
        }

        public async Task<TEntity> GetEntityByIdAsync(int Id)
        {
            return await _appDbContext.Set<TEntity>().Where(u => u.Id == Id && u.IsDeleted == false).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetEntityByIdWithIncludesAsync(int Id, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _appDbContext.Set<TEntity>();
            if (includes != null) 
            {
                foreach(var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return await query.FirstOrDefaultAsync(e => e.Id == Id);
        }

        //public async Task<TEntity> GetUnitByNameAsync(string Name, CancellationToken cancellationToken)
        //{
        //    return await _appDbContext.Set<TEntity>().Where(u => u.Name == Name).FirstOrDefaultAsync(cancellationToken);
        //}

        public async Task<bool> UpdateEntityAsync(TEntity entity)
        {
            var entry = _appDbContext.Set<TEntity>().Update(entity);
            if (entry != null)
            {
                return true;
            }
            return false;
        }

        public async Task<TEntity> GetByExpressionAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _appDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
    }
}
