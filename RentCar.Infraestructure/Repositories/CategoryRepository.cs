using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentCar.domain.Entity;
using RentCar.Infraestructure.Context;
using RentCar.Infraestructure.Core;
using RentCar.Infraestructure.Interfaces;

namespace RentCar.Infraestructure.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly RentCarContext context;
        public CategoryRepository(RentCarContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoryByName(string name)
        {
            return await this.context.Category.Where(c => c.Nombre == name).ToListAsync();
        }

        public async override Task Save(Category entity)
        {
            await base.Save(entity);
            await base.SaveChanges();
        }

        public async override Task Delete(int id)
        {
            if (await this.context.Category.AnyAsync(cd => cd.Id == id))
            {
                await base.Delete(id);
                await base.SaveChanges();
            }
        }
    }
}