using Invoices.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoices.Infrastructure.Persistence;

public class InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : DbContext(options)
{
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceLine> InvoicesLines => Set<InvoiceLine>();
    public DbSet<InvoiceTemplate> InvoicesTemplates => Set<InvoiceTemplate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasIndex(i => i.InvoiceNumber).IsUnique();
            entity.Property(i => i.Status).HasConversion<string>();

            entity.Property(i => i.AmountExclVat).HasPrecision(18, 2);
            entity.Property(i => i.VatAmount).HasPrecision(18, 2);
            entity.Property(i => i.TotalAmount).HasPrecision(18, 2);

            entity.HasOne<InvoiceTemplate>()
            .WithMany()
            .HasForeignKey(i => i.TemplateId);

        });

        modelBuilder.Entity<InvoiceLine>(entity =>
        {
            entity.Property(l => l.Unit).HasConversion<string>();

            entity.Property(l => l.Quantity).HasPrecision(18, 2);
            entity.Property(l => l.UnitPrice).HasPrecision(18, 2);
            entity.Property(l => l.Amount).HasPrecision(18, 2);

            entity.Property(l => l.VatRate).HasPrecision(5, 2);

            entity.HasOne<Invoice>().WithMany().HasForeignKey(l => l.InvoiceId);
        });

            
    }
}
