using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UDC.MerchantApi.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Merchants",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GetUtcDate()");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Merchants",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Merchants");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Merchants",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GetUtcDate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
