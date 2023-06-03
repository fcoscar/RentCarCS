using System.Threading.Tasks;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Context;
using RentCar.Infraestructure.Core;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Infraestructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        private readonly RentCarContext context;
        public UserRepository(RentCarContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task Save(User entity)
        {
            await base.Save(entity);
            await base.SaveChanges();
        }
    }
}