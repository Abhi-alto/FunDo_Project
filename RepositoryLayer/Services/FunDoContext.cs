using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FunDoContext:DbContext
    {
        public FunDoContext(DbContextOptions option) : base(option)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
