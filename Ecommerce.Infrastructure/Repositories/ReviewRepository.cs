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
    internal class ReviewRepository(ApplicationDbContext context):BaseRepository<Review>(context),IReviewRepository
    {
    }
}
