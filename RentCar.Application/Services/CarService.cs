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
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ILogger<CarService> logger;
        private readonly IUserRepository userRepository;
        private readonly IAlquilerRepository alquilerRepository;

        public CarService(ICarRepository carRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            IAlquilerRepository alquilerRepository,
            ILogger<CarService> logger)
        {
            this.carRepository = carRepository;
            this.categoryRepository = categoryRepository;
            this.logger = logger;
            this.userRepository = userRepository;
            this.alquilerRepository = alquilerRepository;
        }

        public async Task<ServiceResult> Get()
        {
            var result = new ServiceResult();
            try
            {
                result.Data = await GetCars();
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniendo los carros";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetById(int id)
        {
            var result = new ServiceResult();
            try
            {
                result.Data = (await GetCars(id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniendo carro por Id";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetByBrand(string brand)
        {
            var result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Brand: brand);
            }
            catch (Exception e)
            {
                result.Message = "Error Obteniendo carro por marca";
                result.Succes = false;
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetByYearRange(int since, int to)
        {
            var result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Since: since, To: to);
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error obtniendo carros por rango de anos";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetByYear(int year)
        {
            var result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Year: year);
            }
            catch (Exception e)
            {
                result.Message = "Error Obteniendo carro por año";
                result.Succes = false;
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> GetByCategory(int categoriaId)
        {
            var result = new ServiceResult();
            try
            {
                result.Data = await GetCars(Category: categoriaId);
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
            var carAddResponse = new CarAddResponse();
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

                var car = carAddDto.ConvertCarAddDtoToCar();
                await carRepository.Save(car);
                carAddResponse.Id = car.Id;
            }
            catch (Exception e)
            {
                carAddResponse.Succes = false;
                carAddResponse.Message = "Error Actualizando carro";
                logger.Log(LogLevel.Error, $"{carAddResponse.Message}", e.ToString());
            }

            return carAddResponse;
        }

        public async Task<ServiceResult> ModifyCar(CarUpdateDto carUpdateDto)
        {
            var carAddResponse = new ServiceResult();
            try
            {
                var car = await carRepository.GetEntityById(carUpdateDto.Id);
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
                await carRepository.Update(car);
            }
            catch (Exception e)
            {
                carAddResponse.Succes = false;
                carAddResponse.Message = "Error Agregando carro";
                logger.Log(LogLevel.Error, $"{carAddResponse.Message}", e.ToString());
            }

            return carAddResponse;
        }

        public Task<ServiceResult> Delete(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<List<CarGetModel>> GetCars(int? Id = null, string? Brand = null, int? Year = null,
            int? Since = null, int? To = null, int? Category = null)
        {
            var Listcars = new List<CarGetModel>();
            try
            {
                Listcars = (from cars in (await carRepository.GetAll())
                    join cat in await categoryRepository.GetAll() on cars.CategoriaId equals cat.Id
                    join user in await userRepository.GetAll() on cars.IdUsuarioCreacion equals user.Id
                    join alq in await alquilerRepository.GetAll() on cars.Id equals alq.CarId into rents
                    where cars.Id == Id || !Id.HasValue
                    where cars.CategoriaId == Category || !Category.HasValue
                    where cars.Marca == Brand || Brand == null
                    where cars.Year == Year || !Year.HasValue
                    where cars.Year >= Since || !Since.HasValue
                    where cars.Year <= To || !To.HasValue
                    select new CarGetModel
                    {
                        Id = cars.Id,
                        Marca = cars.Marca,
                        Modelo = cars.Modelo,
                        Year = cars.Year,
                        Pasajeros = cars.Pasajeros,
                        Descripcion = cars.Descripcion,
                        PricePerDay = cars.PricePerDay,
                        Categoria = cat.Nombre,
                        CategoriaId = cat.Id,
                        Combustible = cars.Combustible,
                        Location = cars.Location,
                        User = user.ConvertUserToUserGetModel(),
                        Alqs = rents.Select(r => r.ConvertAlquilerToAlguilerGetModel()).ToList()
                    }).ToList();
            }
            catch (Exception e)
            {
                Listcars = null;
                logger.Log(LogLevel.Error, "Error obteniendo carros", e.ToString());
            }

            return Listcars;
        }
    }
}