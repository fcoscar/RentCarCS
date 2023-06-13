using System;

namespace RentCar.Application.Dtos.Alquiler
{
    public class AlquilerDto 
    {
        public int? IdUsuario { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CarId { get; set; }
    }
}