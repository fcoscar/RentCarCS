using System.Collections.Generic;
using System.Threading.Tasks;
using RentCar.Application.Core;
using RentCar.Application.Models;

namespace RentCar.Application.Contract
{
    public interface ICarService
    {
        Task<ServiceResult> Get();
        Task<ServiceResult> GetByBrand();
        Task<ServiceResult> GetByName();
    }
}