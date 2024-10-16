﻿using Microsoft.EntityFrameworkCore;
using TgLabApi.Domain.Entities.Common;
using TGLabAPI.Application.Interfaces.Repositories.Common;

namespace TGLabAPI.Infrastructure.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ApiContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(ApiContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T?> Get(object id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _dbSet.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual async Task<T> Insert(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }

        public virtual async Task Update(T entity)
        {
            try
            {
                entity.UpdatedAt = DateTimeOffset.UtcNow;

                _dbSet.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync(); 
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public virtual async Task Delete(T entity)
        {
            try
            {
                entity.DeletedAt = DateTimeOffset.UtcNow;

                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e) 
            {
                throw new Exception(e.Message);
            }
        }
    }
}
