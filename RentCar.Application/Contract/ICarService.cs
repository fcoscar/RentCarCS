using System.Collections.Generic;
using System.Threading.Tasks;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Car;
using RentCar.Application.Models;
using RentCar.Application.Responses;

namespace RentCar.Application.Contract
{
    public interface ICarService
    {
        Task<ServiceResult> Get();
        Task<ServiceResult> GetById(int id);
        Task<ServiceResult> GetByBrand(string brand);
        Task<ServiceResult> GetByYear(int year);
        Task<CarAddResponse> SaveCar(CarAddDto carAddDto);
        Task<CarAddResponse> ModifyCar(CarUpdateDto carUpdateDto);
        Task<ServiceResult> Delete(int id);
    }
}