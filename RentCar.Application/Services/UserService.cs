using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using RentCar.Application.Contract;
using RentCar.Application.Core;
using RentCar.Application.Dtos.Car;
using RentCar.Application.Dtos.User;
using RentCar.Application.Extensions;
using RentCar.Application.Responses;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserService> logger;
        private readonly ICarRepository carRepository;
        private readonly IAlquilerRepository alquilerRepository;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, ICarRepository carRepository, IAlquilerRepository alquilerRepository)
        {
            this.logger = logger;
            this.carRepository = carRepository;
            this.alquilerRepository = alquilerRepository;
            this.userRepository = userRepository;
        }
        public async Task<ServiceResult> GetUsers()
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetUser();
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error obteniedo usuarios";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }
        
        public async Task<ServiceResult> GetById(int id)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                result.Data = await GetUser(id);
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniedo usuarios";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> SaveUser(UserAddDto userDto)
        {
            var result = new ServiceResult();
            try
            {
                User user = userDto.ConvertUserAddDtoToUser();
                await userRepository.Save(user);
            }
            catch (Exception e)
            {
                result.Message = "Error agregando usuario";
                result.Succes = false;
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }
            return result;
        }

        private async Task<List<UserDto>> GetUser(int? id = null)
        {
            List<UserDto> ListUsers = new List<UserDto>();
            try
            {
                ListUsers = (from users in (await this.userRepository.GetAll())
                    join cars in await this.carRepository.GetAll() on users.Id equals cars.IdUsuarioCreacion into carros
                    //join alqs in await this.alquilerRepository.GetAll() on users.Id equals alqs.IdUsuarioCreacion
                    where users.Id == id || !id.HasValue
                    select new UserDto()
                    {
                        Id = users.Id,
                        FirstName = users.FirstName,
                        LastName = users.LastName,
                        Username = users.Username,
                        Password = users.Password,
                        Mail = users.Mail,
                        DocType = users.DocType,
                        NumDoc = users.NumDoc,
                        IsAdmin = users.IsAdmin,
                        FechaCreacion = users.FechaCreacion,
                        //Carros = carros.ToList()
                    }).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return ListUsers;
        }
    }
}