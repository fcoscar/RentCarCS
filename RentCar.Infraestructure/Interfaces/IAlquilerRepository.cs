using RentCar.domain.Entity;
using RentCar.domain.Repository;

namespace RentCar.Infraestructure.Interfaces
{
    public interface IAlquilerRepository : IBaseRepository<Alquiler>
    {
        //metodos exclusivos para alquiler
    }
}