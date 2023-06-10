using RentCar.Application.Dtos.User;
using RentCar.domain.Entity;

namespace RentCar.Application.Extensions
{
    public static class UserExtension
    {
        public static UserDto ConvertUserToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Mail = user.Mail,
                DocType = user.DocType,
                NumDoc = user.NumDoc,
                IsAdmin = user.IsAdmin,
                FechaCreacion = user.FechaCreacion
                // Carros = user.
            };
        }

        public static User ConvertUserAddDtoToUser(this UserAddDto user)
        {
            return new User
            {
                //Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                Mail = user.Mail,
                DocType = user.DocType,
                NumDoc = user.NumDoc,
                IsAdmin = user.IsAdmin,
                FechaCreacion = user.FechaCreacion
            };
        }
    }
}