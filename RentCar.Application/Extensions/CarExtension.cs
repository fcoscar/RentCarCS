using System;
using RentCar.Application.Dtos.Car;
using RentCar.domain.Entity;

namespace RentCar.Application.Extensions
{
    public static class CarExtension
    {
        public static Car ConvertCarAddDtoToCar(this CarAddDto carAddDto)
        {                                       //metodo de extension
            return new Car()
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
        }
    }
}