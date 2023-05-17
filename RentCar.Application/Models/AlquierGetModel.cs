using System;

namespace RentCar.Application.Models
{
    public class AlquierGetModel
    {
        public int Id { get; set; }
        public int ReservationTime { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int Car { get; set; }
    }
}