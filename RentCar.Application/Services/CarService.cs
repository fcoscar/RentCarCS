using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentCar.Application.Contract;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Car;
using RentCar.Application.Models;
using RentCar.Application.Responses;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly ILogger<CarService> logger;
        public CarService(ICarRepository carRepository, ILogger<CarService> logger)
        {
            this.carRepository = carRepository;
            this.logger = logger;
        }
        public async Task<ServiceResult> Get()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var query = (from cars in (await this.carRepository.GetAll())
                        select new Models.CarGetModel()
                        {
                            Id = cars.Id,
                            Marca = cars.Marca,
                            Modelo = cars.Modelo,
                            Year = cars.Year,
                            Pasajeros = cars.Pasajeros,
                            Descripcion = cars.Descripcion,
                            PricePerDay = cars.PricePerDay
                        }).ToList();
                result.Data = query;
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniendo los carros";
                this.logger.Log(LogLevel.Error ,"Error", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var car = await this.carRepository.GetEntityById(id);
                var carModel = new Models.CarGetModel()
                {
                    Id = car.Id,
                    Marca = car.Marca,
                    Modelo = car.Modelo,
                    Year = car.Year,
                    Pasajeros = car.Pasajeros,
                    Descripcion = car.Descripcion,
                    PricePerDay = car.PricePerDay                
                };
                result.Data = carModel;
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniendo carro";
                logger.Log(LogLevel.Error ,$"{result.Message}", e.ToString());
            }

            return result;
        }

        public Task<ServiceResult> GetByBrand(string brand)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> GetByYear(int year)
        {
            throw new System.NotImplementedException();
        }

        public async Task<CarAddResponse> SaveCar(CarAddDto carAddDto)
        {
            CarAddResponse carAddResponse = new CarAddResponse();

            try
            {
                if (string.IsNullOrEmpty(carAddDto.Modelo))
                {
                    carAddResponse.Message = "Necesita Agregar Un Modelo";
                    carAddResponse.Succes = false;
                    return carAddResponse;
                }

                if (carAddDto.Marca.Length > 50)
                {
                    carAddResponse.Message = "Longitud Invalida";
                    carAddResponse.Succes = false;
                    return carAddResponse;
                }

                Car car = new Car()
                {
                    Marca = carAddDto.Marca,
                    Modelo = carAddDto.Modelo,
                    Year = carAddDto.Year,
                    Pasajeros = carAddDto.Pasajeros,
                    Descripcion = carAddDto.Descripcion,
                    PricePerDay = carAddDto.PricePerDay,
                    IdUsuarioCreacion = carAddDto.IdUsuario,
                    FechaCreacion = DateTime.Now,
                    Eliminado = false,
                    IsBusy = false
                };
                await this.carRepository.Save(car);
            }
            catch (Exception e)
            {
                carAddResponse.Succes = false;
                carAddResponse.Message = "Error Agregando carro";
                logger.Log(LogLevel.Error ,$"{carAddResponse.Message}", e.ToString());
            }

            return carAddResponse;
        }

        public Task<CarAddResponse> ModifyCar(CarUpdateDto carUpdateDto)
        {
            throw new System.NotImplementedException();
        }

        public Task<ServiceResult> Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}