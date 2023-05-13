using System.Collections.Generic;
using System.Threading.Tasks;
using RentCar.domain.Entity;
using RentCar.domain.Repository;

namespace RentCar.Infraestructure.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        //Task<IEnumerable<Category>> GetCategoryByName(string name);
        // metodos exclusivos para categorias
    }
}