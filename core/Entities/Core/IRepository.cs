namespace core.Entities.Core;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity?> AddAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task DeleteAsync(int? id);
    Task<TEntity?> GetByIdAsync(int? id);
}