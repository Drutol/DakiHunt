using System;
using System.Collections.Generic;
using System.Text;
using DakiHunt.DataAccess.Interfaces.Service;
using Microsoft.EntityFrameworkCore;

namespace DakiHunt.DataAccess.Entities
{
    public class AppUser : IModelWithRelation
    {
        public string Id { get; set; }


        public static void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
