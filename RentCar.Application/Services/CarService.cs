using System.Threading.Tasks;
using RentCar.Application.Contract;
using RentCar.Application.Core;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        
        public Task<ServiceResult> Get()
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> GetByBrand()
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> GetByName()
        {
            throw new System.NotImplementedException();
        }
    }
}