using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookings.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNameToBookingItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BookingServices",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BookingMachines",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql("UPDATE bm SET Name = m.Name FROM BookingMachines bm JOIN Machines m ON m.Id = bm.MachineId");
            migrationBuilder.Sql("UPDATE bs SET Name = s.Name FROM BookingServices bs JOIN BusinessServices s ON s.Id = bs.ServiceId");
        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "BookingServices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BookingMachines");
        }
    }
}
