using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentCar.Application.Contract;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Car;
using RentCar.Application.Extensions;
using RentCar.Application.Models;
using RentCar.Application.Responses;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogger<CarService> logger;
        public CarService(ICarRepository carRepository,
            ICategoryRepository categoryRepository,
            ILogger<CarService> logger)
        {
            this.carRepository = carRepository;
            this.categoryRepository = categoryRepository;
            this.logger = logger;
        }
        public async Task<ServiceResult> Get()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetCars();
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniendo los carros";
                this.logger.Log(LogLevel.Error ,$"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Id:id);
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniendo carro por Id";
                logger.Log(LogLevel.Error ,$"{result.Message}", e.ToString());
            }
            return result;
        }

        public async Task<ServiceResult> GetByBrand(string brand)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Brand:brand);
            }
            catch (Exception e)
            {
                result.Message = "Error Obteniendo carro por marca";
                result.Succes = false;
                logger.Log(LogLevel.Error,$"{result.Message}", e.ToString());
            }
            return result;
        }

        public async Task<ServiceResult> GetByYearRange(int since, int to)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Since:since,To:to);
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error obtniendo carros por rango de anos";
                this.logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }
            return result;
        }
        public async Task<ServiceResult> GetByYear(int year)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Year:year);
            }
            catch (Exception e)
            {
                result.Message = "Error Obteniendo carro por año";
                result.Succes = false;
                logger.Log(LogLevel.Error,$"{result.Message}", e.ToString());
            }
            return result;
        }

        public async Task<ServiceResult> GetByCategory(int categoriaId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Category:categoriaId);
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error obteniendo carro por categoria";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }
            return result;
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

                Car car = carAddDto.ConvertCarAddDtoToCar();
                await this.carRepository.Save(car);
            }
            catch (Exception e)
            {
                carAddResponse.Succes = false;
                carAddResponse.Message = "Error Actualizando carro";
                logger.Log(LogLevel.Error ,$"{carAddResponse.Message}", e.ToString());
            }
            return carAddResponse;
        }

        public async Task<ServiceResult> ModifyCar(CarUpdateDto carUpdateDto)
        {
            ServiceResult carAddResponse = new ServiceResult();
            try
            {
                Car car = await this.carRepository.GetEntityById(carUpdateDto.Id);
                car.Marca = carUpdateDto.Marca;
                car.Modelo = carUpdateDto.Modelo;
                car.Year = carUpdateDto.Year;
                car.Pasajeros = carUpdateDto.Pasajeros;
                car.Descripcion = carUpdateDto.Descripcion;
                car.PricePerDay = carUpdateDto.PricePerDay;
                car.IsBusy = carUpdateDto.IsBusy;
                car.From = carUpdateDto.From;
                car.To = carUpdateDto.To;
                car.Eliminado = carUpdateDto.Eliminado;
                car.CategoriaId = carUpdateDto.CategoriaId;
                car.FechaMod = DateTime.Now;
                await this.carRepository.Update(car);
            }
            catch (Exception e)
            {
                carAddResponse.Succes = false;
                carAddResponse.Message = "Error Agregando carro";
                logger.Log(LogLevel.Error ,$"{carAddResponse.Message}", e.ToString());
            }
            return carAddResponse;
        }

        public Task<ServiceResult> Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        private async Task<List<CarGetModel>> GetCars(int? Id = null, string? Brand = null, int? Year = null, 
                                                        int? Since = null, int? To = null, int? Category = null)
        {
            List<CarGetModel> Listcars = new List<CarGetModel>();
            try
            {
                Listcars = (from cars in (await this.carRepository.GetAll())
                    join cat in await this.categoryRepository.GetAll() on cars.CategoriaId equals cat.Id
                    where cars.Id == Id || !Id.HasValue
                    where cars.CategoriaId == Category || !Category.HasValue
                    where cars.Marca == Brand || Brand == null
                    where cars.Year == Year || !Year.HasValue
                    where cars.Year >= Since || !Since.HasValue
                    where cars.Year <= To || !To.HasValue
                    select new CarGetModel()
                    {
                        Id = cars.Id,
                        Marca = cars.Marca,
                        Modelo = cars.Modelo,
                        Year = cars.Year,
                        Pasajeros = cars.Pasajeros,
                        Descripcion = cars.Descripcion,
                        PricePerDay = cars.PricePerDay,
                        Categoria = cat.Nombre
                    }).ToList();
            }
            catch (Exception e)
            {
                Listcars = null;
                this.logger.Log(LogLevel.Error, "Error obteniendo carros", e.ToString());
            }
            return Listcars;
        }
    }
}