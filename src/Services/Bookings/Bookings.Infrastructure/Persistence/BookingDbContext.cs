using Bookings.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Bookings.Infrastructure.Persistence;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options)
{
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingEmployee> BookingEmployees => Set<BookingEmployee>();
    public DbSet<BookingMachine> BookingMachines => Set<BookingMachine>();
    public DbSet<BookingService> BookingServices => Set<BookingService>();
    public DbSet<Machine> Machines => Set<Machine>();
    public DbSet<BusinessService> BusinessServices => Set<BusinessService>();
    public DbSet<TimeEntry> TimeEntries => Set<TimeEntry>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Booking
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasIndex(b => b.ProjectId);

            entity.HasIndex(b => b.InvoiceId);
                
            entity.HasIndex(b => b.CustomerId);

            entity.Property(b => b.CreatedAt)
                .IsRequired();
            entity.Property(b => b.StartsAt)
                .IsRequired();
            entity.Property(b => b.EndsAt)
                .IsRequired();

            entity.Property(b => b.Status)
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();
        });

        //BookingEmployee
        modelBuilder.Entity<BookingEmployee>(entity =>
        {
            entity.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(b => b.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(be => new { be.BookingId, be.EmployeeId })
                .IsUnique();
        });

        //BookingMachine
        modelBuilder.Entity<BookingMachine>(entity =>
        {
            entity.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(bm => bm.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(bm => new { bm.BookingId, bm.MachineId })
                .IsUnique();

            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(bm => bm.PricePerDay)
                .HasPrecision(18, 2);

            entity.HasOne<Machine>()
                .WithMany()
                .HasForeignKey(bm => bm.MachineId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //BookingService
        modelBuilder.Entity<BookingService>(entity =>
        {
            entity.HasOne<Booking>()
                .WithMany()
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(bs => new { bs.BookingId, bs.ServiceId })
                .IsUnique();

            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(bs => bs.PricePerHour)
                .HasPrecision(18, 2);

            entity.Property(bs => bs.EstimatedHours)
                .HasPrecision(10, 2);

            entity.HasOne<BusinessService>()
                .WithMany()
                .HasForeignKey(bs => bs.ServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        //Machine
        modelBuilder.Entity<Machine>(entity =>
        {
            entity.Property(m => m.Name).HasMaxLength(200).IsRequired();
            entity.Property(m => m.Type).HasMaxLength(100).IsRequired();
            entity.Property(m => m.RegistrationNumber).HasMaxLength(20).IsRequired();
            entity.HasIndex(m => m.RegistrationNumber).IsUnique();
            entity.Property(m => m.PricePerDay).HasPrecision(18, 2);
        });

        //Service
        modelBuilder.Entity<BusinessService>(entity =>
        {
            entity.Property(s => s.Name).HasMaxLength(200).IsRequired();
            entity.Property(s => s.Description).HasMaxLength(1000);
            entity.Property(s => s.PricePerHour).HasPrecision(18, 2);
            entity.Property(s => s.RequiredTitle).HasMaxLength(50);
        });

        //TimeEntry
        modelBuilder.Entity<TimeEntry>(entity =>
        {
            entity.HasOne<BookingService>()
                .WithMany()
                .HasForeignKey(t => t.BookingServiceId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(t => t.BookingServiceId);
            entity.HasIndex(t => t.EmployeeId);

            entity.Property(t => t.WorkDate)
                .IsRequired();

            entity.Property(t => t.Hours)
                .HasPrecision(5, 2);

            entity.Property(t => t.Note)
                .HasMaxLength(500);

            entity.Property(t => t.CreatedAt)
                .IsRequired();
        });
    }

}
