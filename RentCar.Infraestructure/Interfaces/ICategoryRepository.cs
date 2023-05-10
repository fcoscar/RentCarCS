using RentCar.domain.Entity;
using RentCar.domain.Repository;

namespace RentCar.Infraestructure.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        // metodos exclusivos para categorias
    }
}