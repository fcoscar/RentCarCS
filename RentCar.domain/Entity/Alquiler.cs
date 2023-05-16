using System;
using RentCar.domain.Core;

namespace RentCar.domain.Entity
{
    public class Alquiler : BaseEntity
    {
        public int Id { get; set; }
        public int ReservationTime { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int CarId { get; set; }
    }
}