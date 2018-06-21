using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Entities;

namespace DakiHunt.DataAccess.Interfaces.Service
{
    public interface IUserService : IServiceBase<AppUser,IUserService>
    {
    }
}
