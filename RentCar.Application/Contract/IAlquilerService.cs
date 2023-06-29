using System.Threading.Tasks;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Alquiler;
using RentCar.Application.Responses;

namespace RentCar.Application.Contract
{
    public interface IAlquilerService
    {
        Task<ServiceResult> Get();
        Task<ServiceResult> GetById(int id);
        Task<AlquilerAddResponse> SaveAlquiler(AlquilerDto alquilerAddDto);
        Task<AlquilerAddResponse> ModifyAlq(AlquilerUpdateDto alquler);
    }
}