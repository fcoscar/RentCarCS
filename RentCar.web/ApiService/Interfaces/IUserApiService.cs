using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Interfaces;

public interface IUserApiService
{
    public Task<UserListResponse> GetUsers();
    public Task<UserResponse> GetUser(int id);
    public Task<UserAddResponse> SaveUser(UserSaveRequest userNew);
    public Task<LoginResponse> Login(LoginRequest loginRequest);
}