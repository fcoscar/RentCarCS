using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Interfaces;

public interface IUserApiService
{
    public Task<UserListResponse> GetUsers();
}