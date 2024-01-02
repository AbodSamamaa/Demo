using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class ggkj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Payment_IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_IdUsersNavigationIdUsers",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "IdUsersNavigationIdUsers",
                table: "Purchases",
                newName: "IdTypeDocument");

            migrationBuilder.RenameColumn(
                name: "IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase",
                table: "Purchases",
                newName: "IdPaymentMethod");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_IdUsersNavigationIdUsers",
                table: "Purchases",
                newName: "IX_Purchases_IdTypeDocument");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase",
                table: "Purchases",
                newName: "IX_Purchases_IdPaymentMethod");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdUsers",
                table: "Purchases",
                column: "IdUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Payment_IdPaymentMethod",
                table: "Purchases",
                column: "IdPaymentMethod",
                principalTable: "Payment",
                principalColumn: "IdPayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocument",
                table: "Purchases",
                column: "IdTypeDocument",
                principalTable: "TypeDocumentPurchases",
                principalColumn: "IdTypeDocumentPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_IdUsers",
                table: "Purchases",
                column: "IdUsers",
                principalTable: "Users",
                principalColumn: "idUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Payment_IdPaymentMethod",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocument",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Users_IdUsers",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IdUsers",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "IdTypeDocument",
                table: "Purchases",
                newName: "IdUsersNavigationIdUsers");

            migrationBuilder.RenameColumn(
                name: "IdPaymentMethod",
                table: "Purchases",
                newName: "IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_IdTypeDocument",
                table: "Purchases",
                newName: "IX_Purchases_IdUsersNavigationIdUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_IdPaymentMethod",
                table: "Purchases",
                newName: "IX_Purchases_IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase");

            migrationBuilder.AddColumn<int>(
                name: "IdPaymentMethodNavigationIdPayment",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdPaymentMethodNavigationIdPayment",
                table: "Purchases",
                column: "IdPaymentMethodNavigationIdPayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Payment_IdPaymentMethodNavigationIdPayment",
                table: "Purchases",
                column: "IdPaymentMethodNavigationIdPayment",
                principalTable: "Payment",
                principalColumn: "IdPayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase",
                table: "Purchases",
                column: "IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase",
                principalTable: "TypeDocumentPurchases",
                principalColumn: "IdTypeDocumentPurchase");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Users_IdUsersNavigationIdUsers",
                table: "Purchases",
                column: "IdUsersNavigationIdUsers",
                principalTable: "Users",
                principalColumn: "idUsers");
        }
    }
}
