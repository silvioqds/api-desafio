﻿using API.LuizaLabs.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace API.LuizaLabs.Data
{
    public class SQLContext : DbContext
    {
        public SQLContext() { }

        public SQLContext(DbContextOptions<SQLContext> options) :
            base(options)
        { }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries().Where(x => x.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChanges();
        }
    }
}
