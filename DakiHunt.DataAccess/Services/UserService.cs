using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DakiHunt.DataAccess.Database;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Interfaces;
using DakiHunt.DataAccess.Interfaces.Service;
using DakiHunt.DataAccess.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Services
{
    public class UserService : ServiceBase<AppUser,IUserService> , IUserService
    {
        public UserService(DakiDbContext dbContext) : base(dbContext)
        {
             
        }

        protected override IQueryable<AppUser> Include(IQueryable<AppUser> query)
        {
            return query.Include(user => user.AccountProperties);
        }
    }
}
