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
using RentCar.Application.Models;
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
        public async Task<ServiceResult> Get()
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
                result.Data = (await GetUser(id)).FirstOrDefault();
            }
            catch (Exception e)
            {
                result.Succes = false;
                result.Message = "Error Obteniedo usuarios";
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        public async Task<ServiceResult> SaveUser(UserDto userDto)
        {
            ServiceResult result = new ServiceResult();
            try
            {
                User user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                    Username = userDto.Username,
                    Password = userDto.Password,
                    Mail = userDto.Mail,
                    DocType = userDto.DocType,
                    NumDoc = userDto.NumDoc,
                    IsAdmin = userDto.IsAdmin,
                    IdUsuarioCreacion = userDto.IdUsuario,
                    FechaCreacion = userDto.Fecha,
                };
                await this.userRepository.Save(user);
            }
            catch (Exception e)
            {
                result.Message = "Error agregando usuario";
                result.Succes = false;
                logger.Log(LogLevel.Error, $"{result.Message}", e.ToString());
            }

            return result;
        }

        private async Task<List<UserGetModel>> GetUser(int? id = null)
        {
            List<UserGetModel> ListUsers = new List<UserGetModel>();
            try
            {
                ListUsers = (from users in (await this.userRepository.GetAll())
                    where users.Id == id || !id.HasValue
                    select new UserGetModel()
                    {
                        Id = users.Id,
                        FirstName = users.FirstName,
                        LastName = users.LastName,
                        Username = users.Username,
                        Mail = users.Mail,
                        DocType = users.DocType,
                        NumDoc = users.NumDoc,
                        IsAdmin = users.IsAdmin,
                        FechaCreacion = users.FechaCreacion
                    }).ToList();
            }
            catch (Exception e)
            {
                ListUsers = null;
                logger.Log(LogLevel.Error, "Error obteniendo usuarios", e.ToString());
            }
            return ListUsers;
        }
    }
}