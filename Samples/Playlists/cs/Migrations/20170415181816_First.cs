using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDKTemplate.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    IsVerifiedCustomer = table.Column<bool>(nullable: false),
                    MobileNo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    WalletBalance = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    BarCode = table.Column<string>(nullable: true),
                    DiscountPer = table.Column<float>(nullable: false),
                    DisplayPrice = table.Column<float>(nullable: false),
                    IsInventoryItem = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RefillTime = table.Column<int>(nullable: false),
                    Threshold = table.Column<int>(nullable: false),
                    UserDefinedCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "WholeSellers",
                columns: table => new
                {
                    WholeSellerId = table.Column<Guid>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    IsVerifiedWholeSeller = table.Column<bool>(nullable: false),
                    MobileNo = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    WalletBalance = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholeSellers", x => x.WholeSellerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    CustomerOrderId = table.Column<Guid>(nullable: false),
                    BillAmount = table.Column<float>(nullable: false),
                    CustomerId = table.Column<Guid>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PaidAmount = table.Column<float>(nullable: false),
                    WalletSnapShot = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrders", x => x.CustomerOrderId);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholeSellersOrders",
                columns: table => new
                {
                    WholeSellerOrderId = table.Column<Guid>(nullable: false),
                    BillAmount = table.Column<float>(nullable: false),
                    DueDate = table.Column<DateTime>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    PaidAmount = table.Column<float>(nullable: false),
                    WholeSellerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholeSellersOrders", x => x.WholeSellerOrderId);
                    table.ForeignKey(
                        name: "FK_WholeSellersOrders_WholeSellers_WholeSellerId",
                        column: x => x.WholeSellerId,
                        principalTable: "WholeSellers",
                        principalColumn: "WholeSellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderProducts",
                columns: table => new
                {
                    CustomerOrderProductId = table.Column<Guid>(nullable: false),
                    CustomerOrderId = table.Column<Guid>(nullable: false),
                    DiscountPerSnapShot = table.Column<float>(nullable: false),
                    DisplayCostSnapShot = table.Column<float>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    QuantityPurchased = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderProducts", x => x.CustomerOrderProductId);
                    table.ForeignKey(
                        name: "FK_CustomerOrderProducts_CustomerOrders_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrders",
                        principalColumn: "CustomerOrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerOrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WholeSellersOrderProducts",
                columns: table => new
                {
                    WholeSellerOrderProductId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    PurchasePrice = table.Column<float>(nullable: false),
                    QuantityPurchased = table.Column<int>(nullable: false),
                    WholeSellerOrderId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WholeSellersOrderProducts", x => x.WholeSellerOrderProductId);
                    table.ForeignKey(
                        name: "FK_WholeSellersOrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WholeSellersOrderProducts_WholeSellersOrders_WholeSellerOrderId",
                        column: x => x.WholeSellerOrderId,
                        principalTable: "WholeSellersOrders",
                        principalColumn: "WholeSellerOrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MobileNo",
                table: "Customers",
                column: "MobileNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Name",
                table: "Customers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_CustomerId",
                table: "CustomerOrders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderProducts_CustomerOrderId",
                table: "CustomerOrderProducts",
                column: "CustomerOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderProducts_ProductId",
                table: "CustomerOrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BarCode",
                table: "Products",
                column: "BarCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserDefinedCode",
                table: "Products",
                column: "UserDefinedCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WholeSellers_MobileNo",
                table: "WholeSellers",
                column: "MobileNo");

            migrationBuilder.CreateIndex(
                name: "IX_WholeSellers_Name",
                table: "WholeSellers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WholeSellersOrders_WholeSellerId",
                table: "WholeSellersOrders",
                column: "WholeSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_WholeSellersOrderProducts_ProductId",
                table: "WholeSellersOrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WholeSellersOrderProducts_WholeSellerOrderId",
                table: "WholeSellersOrderProducts",
                column: "WholeSellerOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrderProducts");

            migrationBuilder.DropTable(
                name: "WholeSellersOrderProducts");

            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "WholeSellersOrders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "WholeSellers");
        }
    }
}
