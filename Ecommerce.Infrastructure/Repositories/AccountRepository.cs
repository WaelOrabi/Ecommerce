﻿using Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class AccountRepository(ApplicationDbContext context) : BaseRepository<Account>(context), IAccountRepository
    {
      
    }
}
