using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCar.domain.Repository;
using RentCar.Infraestructure.Context;

namespace RentCar.Infraestructure.Core
{
    public abstract class BaseRepository<TEntity> : domain.Repository.IBaseRepository<TEntity> where TEntity : class
    {
        private readonly RentCarContext context;
        private readonly DbSet<TEntity> myDbSet;

        public BaseRepository(RentCarContext context)
        {
            this.context = context;
            this.myDbSet = this.context.Set<TEntity>();
        }
        
        public async virtual Task<IEnumerable<TEntity>> GetAll()
        {
            return await this.myDbSet.ToListAsync();
        }

        public async virtual Task<TEntity> GetEntityById(int id)
        {
            return await this.myDbSet.FindAsync(id);
        }
                                           
        public async virtual Task<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {                                      
            return await this.myDbSet.FindAsync(filter);
            //se le pasa una expresion lambda como parametro ( c => c.Property == filter )
        }

        public async virtual Task Save(TEntity entity)
        {
            await this.myDbSet.AddAsync(entity);
        }

        public async virtual Task Save(params TEntity[] entities)
        {
            await this.myDbSet.AddRangeAsync(entities);
        }

        public async virtual Task Update(TEntity entity)
        {
            this.myDbSet.Update(entity);
        }

        public async virtual Task Update(params TEntity[] entities)
        {
           this.myDbSet.UpdateRange(entities);
        }

        public async virtual Task<bool> Exist(Expression<Func<TEntity, bool>> filter)
        {
            return await this.myDbSet.AnyAsync(filter);
        }

        public async virtual Task SaveChanges()
        {
            await this.context.SaveChangesAsync();
        }
    }
}