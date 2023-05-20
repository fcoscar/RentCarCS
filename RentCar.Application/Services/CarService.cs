using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentCar.Application.Contract;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Car;
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
                var query = (from cars in (await this.carRepository.GetAll())
                    join cat in await this.categoryRepository.GetAll() on cars.CategoriaId equals cat.Id
                        select new Models.CarGetModel()
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
                result.Data = query;
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
                var query = (from car in (await this.carRepository.GetAll())
                    join cat in await this.categoryRepository.GetAll() on car.CategoriaId equals cat.Id
                    where car.Id == id
                    select new Models.CarGetModel()
                    {
                        Id = car.Id,
                        Marca = car.Marca,
                        Modelo = car.Modelo,
                        Year = car.Year,
                        Pasajeros = car.Pasajeros,
                        Descripcion = car.Descripcion,
                        PricePerDay = car.PricePerDay,
                        Categoria = cat.Nombre
                    }).FirstOrDefault();
                //var car = await this.carRepository.GetEntityById(id);
                result.Data = query;
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
                var query = (from car in (await this.carRepository.GetAll())
                    join cat in await this.categoryRepository.GetAll() on car.CategoriaId equals cat.Id
                    where car.Marca == brand
                    select new Models.CarGetModel()
                    {
                        Id = car.Id,
                        Marca = car.Marca,
                        Modelo = car.Modelo,
                        Year = car.Year,
                        Pasajeros = car.Pasajeros,
                        Descripcion = car.Descripcion,
                        PricePerDay = car.PricePerDay,
                        Categoria = cat.Nombre
                    }).ToList();
                result.Data = query;
            }
            catch (Exception e)
            {
                result.Message = "Error Obteniendo carro por marca";
                result.Succes = false;
                logger.Log(LogLevel.Error,$"{result.Message}", e.ToString());
            }
            return result;
        }

        public async Task<ServiceResult> GetByYear(int year)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var query = (from car in (await this.carRepository.GetAll())
                    join cat in await this.categoryRepository.GetAll() on car.CategoriaId equals cat.Id
                    where car.Year == year
                    select new Models.CarGetModel()
                    {
                        Id = car.Id,
                        Marca = car.Marca,
                        Modelo = car.Modelo,
                        Year = car.Year,
                        Pasajeros = car.Pasajeros,
                        Descripcion = car.Descripcion,
                        PricePerDay = car.PricePerDay,
                        Categoria = cat.Nombre
                    }).ToList();
                result.Data = query;
            }
            catch (Exception e)
            {
                result.Message = "Error Obteniendo carro por marca";
                result.Succes = false;
                logger.Log(LogLevel.Error,$"{result.Message}", e.ToString());
            }
            return result;
        }

        public async Task<ServiceResult> GetByCategoria(int categoriaId)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                var query = (from cars in (await this.carRepository.GetAll())
                        join cat in await  this.categoryRepository.GetAll() on cars.CategoriaId equals cat.Id
                        where cars.CategoriaId == categoriaId
                        select new Models.CarGetModel()
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
                result.Data = query;
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

        public async Task<CarAddResponse> ModifyCar(CarUpdateDto carUpdateDto)
        {
            CarAddResponse carAddResponse = new CarAddResponse();
            try
            {
                Car car = await this.carRepository.GetEntityById(carUpdateDto.Id);
                car.Marca = carUpdateDto.Marca;
                car.Modelo = carUpdateDto.Modelo;
                car.Year = carUpdateDto.Year;
                car.Pasajeros = carUpdateDto.Pasajeros;
                car.Descripcion = carUpdateDto.Descripcion;
                car.PricePerDay = carUpdateDto.PricePerDay;
                car.FechaMod = carUpdateDto.Fecha;
                car.IsBusy = carUpdateDto.IsBusy;
                car.From = carUpdateDto.From;
                car.To = carUpdateDto.To;
                car.Eliminado = carUpdateDto.Eliminado;
                car.CategoriaId = carUpdateDto.CategoriaId;
                await this.carRepository.SaveChanges();
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
    }
}