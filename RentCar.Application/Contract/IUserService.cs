using System.Threading.Tasks;
using RentCar.Application.Core;
using RentCar.Application.Dtos.User;
using RentCar.Application.Responses;

namespace RentCar.Application.Contract
{
    public interface IUserService
    {
        Task<ServiceResult> Get();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> SaveUser(UserDto userDto);
    }
}