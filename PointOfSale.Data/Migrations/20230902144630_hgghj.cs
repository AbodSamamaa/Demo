using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class hgghj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Purchases",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "Purchases",
                newName: "PhoneNo");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Purchases",
                newName: "Note");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Purchases",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "PhoneNo",
                table: "Purchases",
                newName: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Purchases",
                newName: "Notes");
        }
    }
}
