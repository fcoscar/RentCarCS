using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RentCar.domain.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetEntityById(int id);
        Task<TEntity> Find(Expression<Func<TEntity, bool>> filter);
        Task Save(TEntity entity);
        Task Save(params TEntity[] entities);
        Task Update(TEntity entity);
        Task Update(params TEntity[] entities);
        Task<bool> Exist(Expression<Func<TEntity, bool>> filter);
        Task SaveChanges(); //para dar el commit para la base de datos
        Task Delete(int id);
    }
}