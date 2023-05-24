using System.Collections.Generic;
using System.Threading.Tasks;
using RentCar.domain.Entity;
using RentCar.domain.Repository;

namespace RentCar.Infraestructure.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}