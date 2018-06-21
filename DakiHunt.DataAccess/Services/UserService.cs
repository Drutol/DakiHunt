﻿using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Database;
using DakiHunt.DataAccess.Entities;
using DakiHunt.DataAccess.Interfaces;
using DakiHunt.DataAccess.Interfaces.Service;

namespace DakiHunt.DataAccess.Services
{
    public class UserService : ServiceBase<AppUser,IUserService> , IUserService
    {
        public UserService(DakiDbContext dbContext, bool saveOnDispose) : base(dbContext, saveOnDispose)
        {

        }
    }
}
