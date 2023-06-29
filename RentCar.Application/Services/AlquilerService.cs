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
                // Obtener el inicio y final de todas los alquiler existentes en la base de datos para obtner todas las fechas entre ellas
                var prevAlqs = await alquilerRepository.GetAlqsByCarId(car.Id);
                var prevAlqsDates = new List<List<DateTime>>();
                foreach (var alq in prevAlqs)
                {
                    var dateRange = new List<DateTime>(){alq.From, alq.To};
                    prevAlqsDates.Add(dateRange);
                }
                
                var prevDates = await GetDatesBetween((DateTime)car.From , (DateTime) car.To);
                foreach (var dateRange in prevAlqsDates)
                {
                    var newDateRange = await GetDatesBetween(dateRange[0], dateRange[1]);
                    foreach (var date in newDateRange)
                    {
                        if(!prevDates.Contains(date)) prevDates.Add(date);
                    }
                }

                var newAlqDateRange = await GetDatesBetween(alquilerAddDto.From, alquilerAddDto.To);
                
                foreach (var date in newAlqDateRange)
                {
                    if (prevDates.Contains(date))
                    {
                        alquilerAddResponse.Message = "Selecciona otro rango de dias";
                        alquilerAddResponse.Succes = false;
                        return alquilerAddResponse;
                    }
                }
                
                var alquiler = new Alquiler
                {
                    ReservationTime = alquilerAddDto.ReservationTime,
                    IdUsuarioCreacion = alquilerAddDto.IdUsuarioCreacion,
                    From = alquilerAddDto.From,
                    To = alquilerAddDto.To,
                    CarId = alquilerAddDto.CarId,
                    TotalPrice = alquilerAddDto.TotalPrice,
                    Status = 
                        DateTime.Now < alquilerAddDto.From ? "Reservado" : 
                        DateTime.Now > alquilerAddDto.To ? "Terminado" :
                        "Activo"
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

        public async Task<AlquilerAddResponse> ModifyAlq(AlquilerUpdateDto alquiler)
        {
            var result = new AlquilerAddResponse();
            try
            {
                var alqToUpdate = await alquilerRepository.GetEntityById(alquiler.Id);
                alqToUpdate.Status = alquiler.Status;
                await alquilerRepository.Update(alqToUpdate);
            }
            catch(Exception e)
            {
                result.Message = "Error agregando Alquiler";
                result.Succes = false;
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());

            }
            return result;
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
        
        private async Task<List<DateTime>> GetDatesBetween(DateTime start, DateTime end)
        {
            var datesList = new List<DateTime>();
            for (DateTime date = start; date <= end; date = date.AddDays(1))
            {
                datesList.Add(date);
            }
            return  datesList;
        }
    }

}