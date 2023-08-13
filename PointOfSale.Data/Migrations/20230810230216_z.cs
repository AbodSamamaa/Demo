using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class z : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTaxNavigationIdTaxes",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_IdTaxNavigationIdTaxes",
                table: "Product",
                column: "IdTaxNavigationIdTaxes");

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

            migrationBuilder.DropIndex(
                name: "IX_Product_IdTaxNavigationIdTaxes",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "IdTaxNavigationIdTaxes",
                table: "Product");
        }
    }
}
