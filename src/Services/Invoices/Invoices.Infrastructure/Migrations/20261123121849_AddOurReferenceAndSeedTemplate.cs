using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Invoices.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOurReferenceAndSeedTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OurReference",
                table: "InvoicesTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "InvoicesTemplates",
                columns: new[] { "Id", "CompanyAddress", "CompanyBankgiro", "CompanyCity", "CompanyEmail", "CompanyName", "CompanyNumber", "CompanyPhone", "CompanyPostalCode", "Logo", "OurReference", "PrimaryColor", "StandardText", "VatNumber" },
                values: new object[] { new Guid("11111111-1111-1111-1111-111111111111"), "Storgatan 1", "123-4567", "Örebro", "faktura@sok.se", "Skrot & Korn AB", "112233-4455", "070-123 12 33", "111 22", "images/logo.png", "Börje Gunnarsson", "#1f1f1b", "Tack för att du anlitar Skrot & Korn!", "SE112233445501" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "InvoicesTemplates",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DropColumn(
                name: "OurReference",
                table: "InvoicesTemplates");
        }
    }
}
