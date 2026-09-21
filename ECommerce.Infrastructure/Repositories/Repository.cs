using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using ECommerce.Infrastructure.Persistence.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Infrastructure.Repositories
{
    public class Repository<T>(StoreDbContext context) : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ck = default)   
            => await _dbSet.ToListAsync(ck);

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ck = default)    
            => await _dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id, ck);
       
        public void Add(T entity) 
            => _dbSet.Add(entity);

        public void Update(T entity) 
            => _dbSet.Update(entity);

        public void Delete(T entity) 
            => entity.MarkAsDeleted();
    }
}
