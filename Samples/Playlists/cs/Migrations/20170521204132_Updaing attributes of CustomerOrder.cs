using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDKTemplate.Migrations
{
    public partial class UpdaingattributesofCustomerOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillAmount",
                table: "CustomerOrders");

            migrationBuilder.RenameColumn(
                name: "WalletSnapShot",
                table: "CustomerOrders",
                newName: "TotalBillAmount");

            migrationBuilder.RenameColumn(
                name: "PaidAmount",
                table: "CustomerOrders",
                newName: "DiscountedAmount");

            migrationBuilder.AlterColumn<float>(
                name: "UsingWalletAmount",
                table: "CustomerOrders",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<float>(
                name: "AddingMoneyToWallet",
                table: "CustomerOrders",
                nullable: false,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalBillAmount",
                table: "CustomerOrders",
                newName: "WalletSnapShot");

            migrationBuilder.RenameColumn(
                name: "DiscountedAmount",
                table: "CustomerOrders",
                newName: "PaidAmount");

            migrationBuilder.AlterColumn<bool>(
                name: "UsingWalletAmount",
                table: "CustomerOrders",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<bool>(
                name: "AddingMoneyToWallet",
                table: "CustomerOrders",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AddColumn<float>(
                name: "BillAmount",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: 0f);
        }
    }
}
