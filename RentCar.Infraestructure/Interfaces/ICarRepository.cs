using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCar.domain.Entity;
using RentCar.domain.Repository;

namespace RentCar.Infraestructure.Interfaces
{
    public interface ICarRepository : IBaseRepository<Car>
    {
        //Task<IEnumerable<Car>> GetCarsByBrand(string brand);
        //Task<IEnumerable<Car>> GetCarsByYear(int year);

        //metodos exclusivos para cars
    }
}