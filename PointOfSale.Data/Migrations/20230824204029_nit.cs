using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointOfSale.Data.Migrations
{
    /// <inheritdoc />
    public partial class nit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeDocumentPurchases",
                columns: table => new
                {
                    IdTypeDocumentPurchase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDocumentPurchases", x => x.IdTypeDocumentPurchase);
                });

            migrationBuilder.CreateTable(
                name: "Purchases",
                columns: table => new
                {
                    IdPurchase = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BarCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchaseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdTypeDocumentPurchase = table.Column<int>(type: "int", nullable: true),
                    IdUsers = table.Column<int>(type: "int", nullable: true),
                    SellerDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subtotal = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalTaxes = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FinalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase = table.Column<int>(type: "int", nullable: true),
                    IdUsersNavigationIdUsers = table.Column<int>(type: "int", nullable: true),
                    IdCategory = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchases", x => x.IdPurchase);
                    table.ForeignKey(
                        name: "FK_Purchases_Category_IdCategory",
                        column: x => x.IdCategory,
                        principalTable: "Category",
                        principalColumn: "idCategory");
                    table.ForeignKey(
                        name: "FK_Purchases_TypeDocumentPurchases_IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase",
                        column: x => x.IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase,
                        principalTable: "TypeDocumentPurchases",
                        principalColumn: "IdTypeDocumentPurchase");
                    table.ForeignKey(
                        name: "FK_Purchases_Users_IdUsersNavigationIdUsers",
                        column: x => x.IdUsersNavigationIdUsers,
                        principalTable: "Users",
                        principalColumn: "idUsers");
                });

            migrationBuilder.CreateTable(
                name: "DetailPurchases",
                columns: table => new
                {
                    IdDetailSale = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSale = table.Column<int>(type: "int", nullable: true),
                    IdProduct = table.Column<int>(type: "int", nullable: true),
                    BrandProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DescriptionProduct = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryProducty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    IdSaleNavigationIdPurchase = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailPurchases", x => x.IdDetailSale);
                    table.ForeignKey(
                        name: "FK_DetailPurchases_Purchases_IdSaleNavigationIdPurchase",
                        column: x => x.IdSaleNavigationIdPurchase,
                        principalTable: "Purchases",
                        principalColumn: "IdPurchase");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailPurchases_IdSaleNavigationIdPurchase",
                table: "DetailPurchases",
                column: "IdSaleNavigationIdPurchase");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdCategory",
                table: "Purchases",
                column: "IdCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase",
                table: "Purchases",
                column: "IdTypeDocumentPurchaseNavigationIdTypeDocumentPurchase");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_IdUsersNavigationIdUsers",
                table: "Purchases",
                column: "IdUsersNavigationIdUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailPurchases");

            migrationBuilder.DropTable(
                name: "Purchases");

            migrationBuilder.DropTable(
                name: "TypeDocumentPurchases");
        }
    }
}
