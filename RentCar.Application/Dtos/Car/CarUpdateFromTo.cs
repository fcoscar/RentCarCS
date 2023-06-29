using System;

namespace RentCar.Application.Dtos.Car
{
    public class CarUpdateFromTo
    {
        public int Id { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}