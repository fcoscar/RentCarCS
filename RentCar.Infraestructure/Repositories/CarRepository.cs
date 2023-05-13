using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Context;
using RentCar.Infraestructure.Core;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Infraestructure.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        private readonly RentCarContext context;
        public CarRepository(RentCarContext context) : base(context)
        {
            this.context = context;
        }
        // public async Task<IEnumerable<Car>> GetCarsByBrand(string brand)
        // {
        //     return await this.context.Cars.Where(c => c.Marca == brand).ToListAsync();
        // }
        // public async Task<IEnumerable<Car>> GetCarsByYear(int year)
        // {
        //     return await this.context.Cars.Where(c => c.Year == year).ToListAsync();
        // }
    }
}