using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoK.Infrastructure.Identity;
using SoK.Domain.Entities;

namespace SoK.Infrasturcture.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<CustomerAgreement> CustomerAgreements => Set<CustomerAgreement>();
    public DbSet<Template> Templates => Set<Template>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
}
