using System.Collections.Generic;
using System.Threading.Tasks;
using RentCar.domain.Entity;
using RentCar.domain.Repository;
using RentCar.Infraestructure.Models;

namespace RentCar.Infraestructure.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<UserCarModel> GetUserCar(int userId);
    }
}