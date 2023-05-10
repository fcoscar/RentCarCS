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
    }
}