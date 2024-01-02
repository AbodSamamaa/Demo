using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class khkh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Category_IdCategory",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Payment_IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IdCategory",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "BarCode",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IdCategory",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "IdPaymentMethodNavigationIdPayment",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "PhoneNo",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "SellerDocument",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Purchases",
                newName: "CustomerDocument");

            migrationBuilder.RenameColumn(
                name: "SellerName",
                table: "Purchases",
                newName: "ClientName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerDocument",
                table: "Purchases",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "ClientName",
                table: "Purchases",
                newName: "SellerName");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarCode",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdCategory",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPaymentMethodNavigationIdPayment",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNo",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Purchases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerDocument",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Purchases",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdCategory",
                table: "Purchases",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdPaymentMethodNavigationIdPayment",
                table: "Purchases",
                column: "IdPaymentMethodNavigationIdPayment");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Category_IdCategory",
                table: "Purchases",
                column: "IdCategory",
                principalTable: "Category",
                principalColumn: "idCategory");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Payment_IdPaymentMethodNavigationIdPayment",
                table: "Purchases",
                column: "IdPaymentMethodNavigationIdPayment",
                principalTable: "Payment",
                principalColumn: "IdPayment");
        }
    }
}
