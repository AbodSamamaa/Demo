using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class fgkdm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPurchases_Purchases_IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases");

            migrationBuilder.DropIndex(
                name: "IX_DetailPurchases_IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases");

            migrationBuilder.DropColumn(
                name: "IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases");

            migrationBuilder.CreateIndex(
                name: "IX_DetailPurchases_IdPurchase",
                table: "DetailPurchases",
                column: "IdPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPurchases_Purchases_IdPurchase",
                table: "DetailPurchases",
                column: "IdPurchase",
                principalTable: "Purchases",
                principalColumn: "IdPurchase");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetailPurchases_Purchases_IdPurchase",
                table: "DetailPurchases");

            migrationBuilder.DropIndex(
                name: "IX_DetailPurchases_IdPurchase",
                table: "DetailPurchases");

            migrationBuilder.AddColumn<int>(
                name: "IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetailPurchases_IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases",
                column: "IdPurchaseNavigationIdPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_DetailPurchases_Purchases_IdPurchaseNavigationIdPurchase",
                table: "DetailPurchases",
                column: "IdPurchaseNavigationIdPurchase",
                principalTable: "Purchases",
                principalColumn: "IdPurchase");
        }
    }
}
