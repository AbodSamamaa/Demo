using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class jgjj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPurchases_Purchases_IdSaleNavigationIdPurchase",
                table: "DetailPurchases");

            migrationBuilder.RenameColumn(
                name: "IdSaleNavigationIdPurchase",
                table: "DetailPurchases",
                newName: "IdPurchaseNavigationIdPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPurchases_IdSaleNavigationIdPurchase",
                table: "DetailPurchases",
                newName: "IX_DetailPurchases_IdPurchaseNavigationIdPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPurchases_Purchases_IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases",
                column: "IdPurchaseNavigationIdPurchase",
                principalTable: "Purchases",
                principalColumn: "IdPurchase");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPurchases_Purchases_IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases");

            migrationBuilder.RenameColumn(
                name: "IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases",
                newName: "IdSaleNavigationIdPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_DetailPurchases_IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases",
                newName: "IX_DetailPurchases_IdSaleNavigationIdPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPurchases_Purchases_IdSaleNavigationIdPurchase",
                table: "DetailPurchases",
                column: "IdSaleNavigationIdPurchase",
                principalTable: "Purchases",
                principalColumn: "IdPurchase");
        }
    }
}
