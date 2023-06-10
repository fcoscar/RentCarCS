using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCar.domain.Repository;
using RentCar.Infraestructure.Context;

namespace RentCar.Infraestructure.Core
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly RentCarContext context;
        private readonly DbSet<TEntity> myDbSet;

        public BaseRepository(RentCarContext context)
        {
            this.context = context;
            myDbSet = this.context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await myDbSet.ToListAsync();
        }

        public virtual async Task<TEntity> GetEntityById(int id)
        {
            return await myDbSet.FindAsync(id);
        }

        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return await myDbSet.FindAsync(filter);
            //se le pasa una expresion lambda como parametro ( c => c.Property == filter )
        }

        public virtual async Task Save(TEntity entity)
        {
            await myDbSet.AddAsync(entity);
        }

        public virtual async Task Save(params TEntity[] entities)
        {
            await myDbSet.AddRangeAsync(entities);
        }

        public virtual async Task Update(TEntity entity)
        {
            myDbSet.Update(entity);
        }

        public virtual async Task Update(params TEntity[] entities)
        {
            myDbSet.UpdateRange(entities);
        }

        public virtual async Task<bool> Exist(Expression<Func<TEntity, bool>> filter)
        {
            return await myDbSet.AnyAsync(filter);
        }

        public virtual async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            var entity = await myDbSet.FindAsync(id);
            myDbSet.Remove(entity);
        }
    }
}