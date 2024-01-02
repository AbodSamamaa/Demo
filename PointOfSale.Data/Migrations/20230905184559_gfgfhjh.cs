using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class gfgfhjh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalPrice",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "IdProduct",
                table: "DetailPurchases",
                newName: "IdCategory");

            migrationBuilder.RenameColumn(
                name: "DescriptionProduct",
                table: "DetailPurchases",
                newName: "DescriptionCategory");

            migrationBuilder.RenameColumn(
                name: "CategoryProducty",
                table: "DetailPurchases",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "BrandProduct",
                table: "DetailPurchases",
                newName: "BrandCategory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdCategory",
                table: "DetailPurchases",
                newName: "IdProduct");

            migrationBuilder.RenameColumn(
                name: "DescriptionCategory",
                table: "DetailPurchases",
                newName: "DescriptionProduct");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "DetailPurchases",
                newName: "CategoryProducty");

            migrationBuilder.RenameColumn(
                name: "BrandCategory",
                table: "DetailPurchases",
                newName: "BrandProduct");

            migrationBuilder.AddColumn<decimal>(
                name: "FinalPrice",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
