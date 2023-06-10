using System;

namespace RentCar.Infraestructure.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Year { get; set; }
        public int Pasajeros { get; set; }
        public string? Descripcion { get; set; }
        public decimal PricePerDay { get; set; }
        public bool? IsBusy { get; set; } = false;
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public DateTime? FechaElimino { get; set; }
        public bool? Eliminado { get; set; } = false;
        public int CategoriaId { get; set; }
    }
}