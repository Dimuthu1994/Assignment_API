using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment_API.Migrations
{
    /// <inheritdoc />
    public partial class SeedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "Active", "Created", "LowestPrice", "ProductName", "RetailPrice", "SKU", "SalePrice" },
                values: new object[] { 1, true, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 200000.0, "Apple iPhone 12", 220000.0, "3852433-fg", 220000.0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);
        }
    }
}
