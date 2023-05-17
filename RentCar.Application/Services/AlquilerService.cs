using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentCar.Application.Contract;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Alquiler;
using RentCar.Application.Responses;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Application.Services
{
    public class AlquilerService : IAlquilerService
    {
        private readonly IAlquilerRepository alquilerRepository;
        private readonly ICarRepository carRepository;
        private readonly ILogger<AlquilerService> logger;
        
        public AlquilerService(IAlquilerRepository alquilerRepository, 
            ICarRepository carRepository,
            ILogger<AlquilerService> logger)
        {
            this.alquilerRepository = alquilerRepository;
            this.carRepository = carRepository;
            this.logger = logger;
        }
        
        public async Task<ServiceResult> Get()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var query = (from alquiler in (await this.alquilerRepository.GetAll())
                    join car in await this.carRepository.GetAll() on alquiler.CarId equals car.Id
                        select new Models.AlquierGetModel()
                        {
                            Id = alquiler.Id,
                            ReservationTime = alquiler.ReservationTime,
                            TotalPrice = alquiler.TotalPrice,
                            From = alquiler.From,
                            To = alquiler.To,
                            Car = car.Id

                        }).ToList();
                result.Data = query;
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error obteniendo Alquileres";
                this.logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AlquilerAddResponse> SaveAlquiler(AlquilerDto alquilerAddDto)
        {
            AlquilerAddResponse alquilerAddResponse = new AlquilerAddResponse();
            try
            {
                var car = await this.carRepository.GetEntityById(alquilerAddDto.CarId);
                var reservationTime = (alquilerAddDto.To.Date - alquilerAddDto.From.Date).Days + 1;
                Alquiler alquiler = new Alquiler()
                {
                    ReservationTime = reservationTime,
                    IdUsuarioCreacion = alquilerAddDto.IdUsuario,
                    From = alquilerAddDto.From,
                    To = alquilerAddDto.To,
                    CarId = alquilerAddDto.CarId,
                    FechaCreacion = DateTime.Now,
                    TotalPrice = car.PricePerDay * reservationTime
                };
                car.From = alquilerAddDto.From;
                car.To = alquilerAddDto.To;
                car.IsBusy = true;
                await this.carRepository.SaveChanges();
                await this.alquilerRepository.Save(alquiler);

            }
            catch (Exception e)
            {
                alquilerAddResponse.Message = "Error agregando Alquiler";
                alquilerAddResponse.Succes = false;
                this.logger.Log(LogLevel.Error, $"{alquilerAddResponse.Message}", e.ToString());
            }
            return alquilerAddResponse;
        }
    }
}