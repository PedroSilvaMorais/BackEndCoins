using Coins.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coins.Data.Context
{
    public class MoedasDbContext : DbContext
    {
        public MoedasDbContext(DbContextOptions options) : base (options)
        {

        }

        public DbSet<Coin> Moeda { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoedasDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
