using RentCar.Application.Models;
using RentCar.domain.Entity;

namespace RentCar.Application.Extensions
{
    public static class AlquilerExtension
    {
        public static AlquierGetModel ConvertAlquilerToAlguilerGetModel(this Alquiler alquiler)
        {
            return new AlquierGetModel()
            {
                Id = alquiler.Id,
                ReservationTime = alquiler.ReservationTime,
                TotalPrice = alquiler.TotalPrice,
                From = alquiler.From,
                To = alquiler.To,
                Car = alquiler.CarId,
                Status = alquiler.Status
            };
        }
    }
}