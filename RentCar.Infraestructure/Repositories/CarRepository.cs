using System;
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
        public async Task<IEnumerable<Car>> GetCarsByBrand(string brand)
        {
            return await this.context.Car.Where(c => c.Marca == brand).ToListAsync();
        }
        public async Task<IEnumerable<Car>> GetCarsByYear(int year)
        {
            return await this.context.Car.Where(c => c.Year == year).ToListAsync();
        }
        public async Task<IEnumerable<Car>> GetCarsByYearRange(int from, int to)
        {
            return await this.context.Car.Where(c => c.Year > from && c.Year < to).ToListAsync();
        }

        public override async Task Save(Car entity)
        {
            await base.Save(entity);
            await base.SaveChanges();
        }

        public override async Task Update(Car entity)
        {
            await base.Update(entity);
            await base.SaveChanges();
        }

        public override async Task Delete(int id)
        {
            if (await this.context.Car.AnyAsync(cd => cd.Id == id))
            {
                await base.Delete(id);
                await base.SaveChanges();
            }
        }
    }
}