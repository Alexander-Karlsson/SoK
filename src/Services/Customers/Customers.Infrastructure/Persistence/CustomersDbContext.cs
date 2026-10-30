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

            // Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(c => c.CustomerNumber)
                    .IsUnique();

                entity.Property(c => c.CustomerNumber)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(c => c.CustomerType)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(c => c.FirstName)
                .HasMaxLength(200);

                entity.Property(c => c.LastName)
                    .HasMaxLength(100);

                entity.Property(c => c.CompanyName)
                    .HasMaxLength(200);

                entity.Property(c => c.OrganisationNumber)
                    .HasMaxLength(20);

                entity.Property(c => c.ContactPerson)
                    .HasMaxLength(200);

                entity.Property(c => c.Email)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(c => c.Phone)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(c => c.Address)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(c => c.City)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(c => c.PostalCode)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(c => c.Preferences)
                    .HasMaxLength(2000);
            });

            // Customer Agreement
            modelBuilder.Entity<CustomerAgreement>(entity =>
            {
                entity.HasOne<Customer>()
                    .WithMany()
                    .HasForeignKey(c => c.CustomerId);

                entity.Property(c => c.Terms)
                    .HasMaxLength(4000)
                    .IsRequired();

                entity.Property(c => c.PaymentTermsDays)
                    .HasDefaultValue(30);

                entity.Property(c => c.ValidFrom)
                    .IsRequired();
            });

            // Project
            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasOne<Customer>()
                    .WithMany()
                    .HasForeignKey(p => p.CustomerId);
                
                entity.Property(p => p.ProjectName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(p => p.WorkAddress)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(p => p.StartDate)
                    .IsRequired();
            });
        }
    }
}
