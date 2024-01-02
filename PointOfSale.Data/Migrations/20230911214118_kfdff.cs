using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class kfdff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocument",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IdTypeDocument",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IdTypeDocument",
                table: "Purchases");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdTypeDocumentPurchase",
                table: "Purchases",
                column: "IdTypeDocumentPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocumentPurchase",
                table: "Purchases",
                column: "IdTypeDocumentPurchase",
                principalTable: "TypeDocumentPurchases",
                principalColumn: "IdTypeDocumentPurchase");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocumentPurchase",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IdTypeDocumentPurchase",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "IdTypeDocument",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdTypeDocument",
                table: "Purchases",
                column: "IdTypeDocument");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocument",
                table: "Purchases",
                column: "IdTypeDocument",
                principalTable: "TypeDocumentPurchases",
                principalColumn: "IdTypeDocumentPurchase");
        }
    }
}
