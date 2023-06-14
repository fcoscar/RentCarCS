using System;

namespace RentCar.Application.Dtos.Alquiler
{
    public class AlquilerDto 
    {
        public int? IdUsuarioCreacion { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int ReservationTime { get; set; }
        public decimal TotalPrice { get; set; }
        public int CarId { get; set; }
    }
}