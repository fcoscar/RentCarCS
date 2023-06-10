using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Interfaces;

public interface IUserApiService
{ 
    Task<UserListResponse> GetUsers();
    Task<UserAddResponse> SaveUser(UserSaveRequest newUser);
}