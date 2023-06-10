using RentCar.Application.Models;
using RentCar.domain.Entity;

namespace RentCar.Application.Extensions
{
    public static class UserExtensions
    {
        public static UserGetModel ConvertUserToUserGetModel(this User user)
        {
            return new UserGetModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Mail = user.Mail,
                DocType = user.DocType,
                NumDoc = user.NumDoc,
                IsAdmin = user.IsAdmin,
                FechaCreacion = user.FechaCreacion
            };
        }
    }
}