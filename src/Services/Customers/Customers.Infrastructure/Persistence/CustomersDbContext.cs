using Customers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Customers.Infrastructure.Persistance
{
    public class CustomersDbContext(DbContextOptions<CustomersDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<CustomerAgreement> CustomerAgreements => Set<CustomerAgreement>();
        public DbSet<Project> Projects => Set<Project>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>(entity =>
            {

            });

            modelBuilder.Entity<CustomerAgreement>(entity =>
            {

            });

            modelBuilder.Entity<Project>(entity =>
            {

            });
        }
    }
}
