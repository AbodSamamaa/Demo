using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class ff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Tax_IdTaxes",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "IdTaxes",
                table: "Product",
                newName: "IdTaxNavigationIdTaxes");

            migrationBuilder.RenameIndex(
                name: "IX_Product_IdTaxes",
                table: "Product",
                newName: "IX_Product_IdTaxNavigationIdTaxes");

            migrationBuilder.AddColumn<int>(
                name: "IdTax",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Tax_IdTaxNavigationIdTaxes",
                table: "Product",
                column: "IdTaxNavigationIdTaxes",
                principalTable: "Tax",
                principalColumn: "IdTaxes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Tax_IdTaxNavigationIdTaxes",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdTax",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "IdTaxNavigationIdTaxes",
                table: "Product",
                newName: "IdTaxes");

            migrationBuilder.RenameIndex(
                name: "IX_Product_IdTaxNavigationIdTaxes",
                table: "Product",
                newName: "IX_Product_IdTaxes");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Tax_IdTaxes",
                table: "Product",
                column: "IdTaxes",
                principalTable: "Tax",
                principalColumn: "IdTaxes");
        }
    }
}
