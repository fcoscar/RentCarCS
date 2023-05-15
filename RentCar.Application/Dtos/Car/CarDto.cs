using System;

namespace RentCar.Application.Dtos.Car
{
    public class CarDto : DtoBase
    {
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Year { get; set; }
        public int Pasajeros { get; set; }
        public string? Descripcion { get; set; }
        public decimal PricePerDay { get; set; }
    }
}