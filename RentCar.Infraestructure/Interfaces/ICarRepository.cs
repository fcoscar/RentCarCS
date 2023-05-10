using RentCar.domain.Entity;
using RentCar.domain.Repository;

namespace RentCar.Infraestructure.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        //metodos exclusivos para cars
    }
}