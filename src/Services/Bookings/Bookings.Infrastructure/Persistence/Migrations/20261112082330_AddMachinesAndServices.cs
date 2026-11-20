using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddMachinesAndServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsOutOfService = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PricePerHour = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingServices_ServiceId",
                table: "BookingServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingMachines_MachineId",
                table: "BookingMachines",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Machines_RegistrationNumber",
                table: "Machines",
                column: "RegistrationNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingMachines_Machines_MachineId",
                table: "BookingMachines",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingServices_Services_ServiceId",
                table: "BookingServices",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingMachines_Machines_MachineId",
                table: "BookingMachines");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingServices_Services_ServiceId",
                table: "BookingServices");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_BookingServices_ServiceId",
                table: "BookingServices");

            migrationBuilder.DropIndex(
                name: "IX_BookingMachines_MachineId",
                table: "BookingMachines");
        }
    }
}
