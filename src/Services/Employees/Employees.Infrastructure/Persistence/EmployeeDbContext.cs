using Employees.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Employees.Infrastructure.Persistence;

public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<EmployeePayroll> EmployeePayrolls => Set<EmployeePayroll>();
    public DbSet<Absence> Absences => Set<Absence>();
    public DbSet<Schedule> Schedules => Set<Schedule>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the Employee entity
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>(entity =>
        {
            // Has unique properties
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.EmployeeNumber)
                .IsUnique();

            entity.HasIndex(e => e.PersonalNumber)
                .IsUnique();

            entity.HasIndex(e => e.UserId)
                .IsUnique();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            // Shows the enum as a string in the database instead of an int number
            entity.Property(entity => entity.EmployeeType)
            .HasConversion<string>()
            .IsRequired();

            entity.Property(entity => entity.Department)
            .HasConversion<string>()
            .IsRequired();

            // Configure other properties as needed
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
        });

        // Configure the Employee Absence
        modelBuilder.Entity<Absence>(entity =>
        {
            entity.HasKey(a => a.Id);

            // Shows the enum as a string in the database instead of an int number
            entity.Property(a => a.AbsenceType)
            .HasConversion<string>();

            // configurate Property 
            entity.Property(a => a.Reason)
            .IsRequired()
            .HasMaxLength(500);

            // Configure the relationship with Employee
            entity.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure the Employee Schedule
        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.BreakMinutes)
            .HasPrecision(5, 2);

            // Configure the relationship with Employee
            entity.HasOne<Employee>()
                .WithMany()
                .HasForeignKey(s => s.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure the Employee Payroll
        modelBuilder.Entity<EmployeePayroll>(entity =>
        {
            entity.HasKey(p => p.Id);

            // Has unique properties
            entity.HasIndex(p => p.EmployeeId)
                .IsUnique();

            // Money properties
            entity.Property(p => p.Salary)
            .HasPrecision(18, 2)
            .IsRequired();

            entity.Property(p => p.Bonus)
            .HasPrecision(18, 2);

            entity.Property(p => p.Deductions)
            .HasPrecision(18, 2);

            // Bank information properties
            entity.Property(p => p.BankName)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(p => p.BankAccount)
            .IsRequired()
            .HasMaxLength(50);

            // Configure the relationship with Employee
            entity.HasOne<Employee>()
                .WithOne()
                .HasForeignKey<EmployeePayroll>(p => p.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
