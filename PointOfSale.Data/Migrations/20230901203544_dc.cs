using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class dc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Payment_IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IdPaymentMethodNavigationIdPayment",
                table: "Purchases");
        }
    }
}
