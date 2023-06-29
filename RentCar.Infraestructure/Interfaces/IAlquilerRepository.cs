using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RentCar.domain.Entity;
using RentCar.domain.Repository;

namespace RentCar.Infraestructure.Interfaces
{
    public interface IAlquilerRepository : IBaseRepository<Alquiler> 
    {
        //metodos exclusivos para alquiler
        Task<IEnumerable<Alquiler>> GetAlqsByCarId(int carId);
    }
}