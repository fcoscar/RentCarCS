using System.Threading.Tasks;
using RentCar.Application.Core;
using RentCar.Application.Dtos.User;
using RentCar.Application.Models;
using RentCar.Application.Responses;

namespace RentCar.Application.Contract
{
    public interface IUserService
    {
        Task<ServiceResult> GetUsers();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> GetUserLogInfo(GetUserInfo userInfo);
        Task<UserAddResponse> SaveUser(UserAddDto userDto);
    }
}