using System;

namespace RentCar.Application.Dtos.Car
{
    public class CarUpdateDto : CarDto
    {
        public int Id { get; set; }
        public bool IsBusy { get; set; } = false;
        public DateTime From { get; set; }
        public DateTime To { get; set; } 
    }
}