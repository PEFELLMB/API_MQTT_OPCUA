using core.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext Context;
    protected readonly DbSet<TEntity> Set;
    
    public Repository(AppDbContext context)
    {
        Context = context;
        Set = context.Set<TEntity>();
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        TEntity? entityOld = await Set.FindAsync(entity.Id);
        
        if (entityOld is null)
            return null;
        
        Context.Entry(entity).State = EntityState.Modified;
        await SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int? id)
    {
        if (id is null) return;
        
        TEntity? entity = await Set.FindAsync(id);

        if (entity is null) return;
        
        Context.Entry(entity).State = EntityState.Deleted;
        await SaveChangesAsync();
    }

    public virtual async Task<TEntity?> GetByIdAsync(int? id)
    {
        if (id is null) return null;
        
        TEntity? entity = await Set.FindAsync(id);

        return entity;
    }

    public async Task<TEntity?> AddAsync(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Added;
        entity.CreationDate = DateTime.UtcNow;

        await Set.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }
    
    protected async Task SaveChangesAsync()
    {
        await Context.SaveChangesAsync();
        Context.ChangeTracker.Clear();
    }
}