using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentCar.Application.Contract;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Alquiler;
using RentCar.Application.Models;
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
            var result = new ServiceResult();
            try
            {
                result.Data = await GetAlquileres();
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error obteniendo Alquileres";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            var result = new ServiceResult();
            try
            {
                result.Data = (await GetAlquileres(id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error obtneniendo alquiler por id";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<AlquilerAddResponse> SaveAlquiler(AlquilerDto alquilerAddDto)
        {
            var alquilerAddResponse = new AlquilerAddResponse();
            try
            {
                var car = await carRepository.GetEntityById(alquilerAddDto.CarId);
                var alquiler = new Alquiler
                {
                    ReservationTime = alquilerAddDto.ReservationTime,
                    IdUsuarioCreacion = alquilerAddDto.IdUsuarioCreacion,
                    From = alquilerAddDto.From,
                    To = alquilerAddDto.To,
                    CarId = alquilerAddDto.CarId,
                    TotalPrice = alquilerAddDto.TotalPrice
                };
                car.From = alquilerAddDto.From;
                car.To = alquilerAddDto.To;
                car.IsBusy = true;
                await carRepository.SaveChanges();
                await alquilerRepository.Save(alquiler);
                alquilerAddResponse.Id = alquiler.Id;
            }
            catch (Exception e)
            {
                alquilerAddResponse.Message = "Error agregando Alquiler";
                alquilerAddResponse.Succes = false;
                logger.Log(LogLevel.Error, $"{alquilerAddResponse.Message}", e.ToString());
            }

            return alquilerAddResponse;
        }

        private async Task<List<AlquierGetModel>> GetAlquileres(int? id = null)
        {
            var ListAlquilers = new List<AlquierGetModel>();
            try
            {
                ListAlquilers = (from alquiler in await alquilerRepository.GetAll()
                    join car in await carRepository.GetAll() on alquiler.CarId equals car.Id
                    where alquiler.Id == id || !id.HasValue
                    select new AlquierGetModel
                    {
                        Id = alquiler.Id,
                        ReservationTime = alquiler.ReservationTime,
                        TotalPrice = alquiler.TotalPrice,
                        From = alquiler.From,
                        To = alquiler.To,
                        Car = car.Id
                    }).ToList();
            }
            catch (Exception e)
            {
                ListAlquilers = null;
                logger.Log(LogLevel.Error, "Error obtniendo alquileres", e.ToString());
            }

            return ListAlquilers;
        }
    }
}