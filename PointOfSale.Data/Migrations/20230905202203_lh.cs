using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class lh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdSale",
                table: "DetailPurchases",
                newName: "IdPurchase");

            migrationBuilder.RenameColumn(
                name: "IdDetailSale",
                table: "DetailPurchases",
                newName: "IdDetailPurchase");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdPurchase",
                table: "DetailPurchases",
                newName: "IdSale");

            migrationBuilder.RenameColumn(
                name: "IdDetailPurchase",
                table: "DetailPurchases",
                newName: "IdDetailSale");
        }
    }
}
