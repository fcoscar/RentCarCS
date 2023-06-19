using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Context;
using RentCar.Infraestructure.Core;
using RentCar.Infraestructure.Interfaces;
using RentCar.Infraestructure.Models;

namespace RentCar.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private ILogger<UserRepository> logger;
        private readonly RentCarContext context;

        public UserRepository(RentCarContext context, ILogger<UserRepository> logger) : base(context)
        {
            this.logger = logger;
            this.context = context;
        }

        public override async Task Save(User entity)
        {
            await base.Save(entity);
            await base.SaveChanges();
        }

        public async Task<UserModel> GetUser(string mail, string pwd)
        {
            var userModel = new UserModel();
            try
            {
                User user = await context.User.SingleOrDefaultAsync(us => us.Mail == mail && us.Password == Encript.GetSHA512(pwd));
                userModel = new UserModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Mail = user.Mail,
                    DocType = user.DocType,
                    NumDoc = user.NumDoc,
                    IsAdmin = user.IsAdmin
                };
            }
            catch (Exception e)
            {
                logger.Log(LogLevel.Error, e.Message, e.ToString());
            }

            return userModel;
        }
    }
}