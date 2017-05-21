using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SDKTemplate.Migrations
{
    public partial class AddingAttributestoorderentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddingMoneyToWallet",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaidNow",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUseWallet",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<float>(
                name: "PartiallyPaid",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PayingLater",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "PayingNow",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<bool>(
                name: "UsingWalletAmount",
                table: "CustomerOrders",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddingMoneyToWallet",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "IsPaidNow",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "IsUseWallet",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "PartiallyPaid",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "PayingLater",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "PayingNow",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "UsingWalletAmount",
                table: "CustomerOrders");
        }
    }
}
