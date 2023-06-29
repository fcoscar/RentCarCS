using System;
using System.Collections.Generic;

namespace RentCar.Application.Models
{
    public class CarGetModel
    {
        public int Id { get; set; }
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public int Year { get; set; }
        public int Pasajeros { get; set; }
        public string? Descripcion { get; set; }
        public decimal PricePerDay { get; set; }
        public string? Categoria { get; set; }
        public int CategoriaId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Combustible { get; set; }
        public string Location { get; set; }
        public UserGetModel User { get; set; }
        public List<AlquierGetModel> Alqs { get; set; }
    }
}