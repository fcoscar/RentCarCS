using RentCar.web.Models;
using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Interfaces;

public interface IAlquilerApiService
{
    Task<AlquilerListResponse> GetAlquileres();
    Task<AlquilerResponse> GetAlquiler(int id);
    Task<AlquilerAddRespoonse> SaveAlquiler(AlquilerAddResquest newAlquiler);
}