using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employees.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SplittingEmployeePayroll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "Deductions",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "PayrollDate",
                table: "EmployeePayrolls");

            migrationBuilder.DropColumn(
                name: "TaxTable",
                table: "EmployeePayrolls");

            migrationBuilder.AddColumn<decimal>(
                name: "TaxRate",
                table: "EmployeePayrolls",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxRate",
                table: "EmployeePayrolls");

            migrationBuilder.AddColumn<decimal>(
                name: "Bonus",
                table: "EmployeePayrolls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Deductions",
                table: "EmployeePayrolls",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "PayrollDate",
                table: "EmployeePayrolls",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "TaxTable",
                table: "EmployeePayrolls",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
