using System;
using RentCar.domain.Core;

namespace RentCar.domain.Entity
{
    public class Car : BaseEntity
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
        public string? Combustible { get; set; }
        public string? Location { get; set; }
    }
}