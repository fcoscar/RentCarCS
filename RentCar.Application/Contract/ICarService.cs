using System.Threading.Tasks;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Car;
using RentCar.Application.Responses;

namespace RentCar.Application.Contract
{
    public interface ICarService
    {
        Task<ServiceResult> Get();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> GetByBrand(string brand);
        Task<ServiceResult> GetByYear(int year);
        Task<ServiceResult> GetByCategory(int category);
        Task<ServiceResult> GetByYearRange(int from, int to);
        Task<CarAddResponse> SaveCar(CarAddDto carAddDto);
        Task<ServiceResult> ModifyCar(CarUpdateDto carUpdateDto);
        Task<ServiceResult> ModifyCarFromTo(CarUpdateFromTo carUpdateFromTo);
        Task<ServiceResult> Delete(int id);
    }
}