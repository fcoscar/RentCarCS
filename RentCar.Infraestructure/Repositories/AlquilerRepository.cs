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
    public class AlquilerRepository : BaseRepository<Alquiler>, IAlquilerRepository
    {
        private readonly RentCarContext context;

        public AlquilerRepository(RentCarContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task Save(Alquiler entity)
        {
            await base.Save(entity);
            await base.SaveChanges();
        }
        
        public override async Task Update(Alquiler entity)
        {
            await base.Update(entity);
            await base.SaveChanges();
        }

        public  async Task<IEnumerable<Alquiler>> GetAlqsByCarId(int carId)
        {
            return await context.Alquiler.Where(a => a.CarId == carId).ToListAsync();
        }
    }
}