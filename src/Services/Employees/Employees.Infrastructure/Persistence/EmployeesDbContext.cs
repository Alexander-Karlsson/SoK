using Employees.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Persistence;

public class EmployeesDbContext(DbContextOptions<EmployeesDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeePayroll> EmployeePayrolls => Set<EmployeePayroll>();
    public DbSet<Absence> Absences => Set<Absence>();
    public DbSet<Schedule> Schedules => Set<Schedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Gör det möjligt att använda en sekvens för att generera unika anställningsnummer
        modelBuilder.HasSequence<int>("EmployeeNumber")
            .StartsAt(1)
            .IncrementsBy(1);

        modelBuilder.Entity<Employee>(entity =>
        {
            // Dessa har unika index för att säkerställa att det inte finns några dubbletter i databasen för dessa fält.
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.EmployeeNumber)
                .IsUnique();

            entity.HasIndex(e => e.PersonalNumber)
                .IsUnique();

            entity.HasIndex(e => e.UserId)
                .IsUnique();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            // Vissar enum värden som strämgar i databasen istället för nummer.
            entity.Property(entity => entity.EmployeeType)
            .HasConversion<string>()
            .IsRequired();

            entity.Property(entity => entity.Department)
            .HasConversion<string>()
            .IsRequired();

            // Konfigurerar övriga egenskaper vid behov
            entity.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(50);

            entity.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(50);

            entity.Property(e => e.PersonalNumber)
            .IsRequired()
            .HasMaxLength(20);

            entity.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(e => e.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

            entity.Property(e => e.Title)
            .HasMaxLength(100);

            // Genererar automatiskt ett unikt anställningsnummer via SQL-sekvens vid skapande av ny post
            entity.Property(entity => entity.EmployeeNumber)
            .HasDefaultValueSql("NEXT VALUE FOR EmployeeNumber");

        });

        // Konfigurerar den anställdas frånvaro
        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.AbsenceType)
            .HasConversion<string>();

            entity.Property(a => a.Reason)
            .IsRequired()
            .HasMaxLength(500);

            // Konfigurerar relationen mellan Absence och Employee
            entity.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Konfigurerar den anställdas schema
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(s => s.Id);

            // Konfigurerar relationen mellan Schedule och Employee
            entity.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Konfigurerar lönehanteringen för den anställda
        modelBuilder.Entity<EmployeePayroll>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.HasIndex(p => p.EmployeeId)
                .IsUnique();

            entity.Property(p => p.Salary)
            .HasPrecision(18, 2)
            .IsRequired();

            entity.Property(p => p.TaxRate)
            .HasPrecision(5, 2)
            .IsRequired();

            entity.Property(p => p.BankName)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(p => p.BankAccount)
            .IsRequired()
            .HasMaxLength(50);

            // Konfigurerar relationen mellan EmployeePayroll och Employee
            entity.HasOne<Employee>()
                .WithOne()
                .HasForeignKey<EmployeePayroll>(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
