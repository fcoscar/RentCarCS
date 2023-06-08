using RentCar.web.Models.Request;
using RentCar.web.Models.Responses;

namespace RentCar.web.ApiService.Interfaces;

public interface ICarApiService
{
    Task<CarListResponse?> GetCars();
    Task<CarGetResponse> GetCar(int id);
    Task<CarAddResponse> SaveCar(CarSaveRequest newCar);
    Task<BaseResponse> UpdateCar(CarSaveRequest carToUpdate);
}