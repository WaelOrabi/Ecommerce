using Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CategoryRepository(ApplicationDbContext context):BaseRepository<Category>(context),ICategoryRepository
    {
    }
}
