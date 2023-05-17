using System;

namespace RentCar.Application.Dtos.Alquiler
{
    public class AlquilerDto : DtoBase
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CarId { get; set; }
    }
}