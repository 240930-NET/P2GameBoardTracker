using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2.API.Migrations
{
    /// <inheritdoc />
    public partial class timeLogged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastLoginDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2024, 11, 4, 5, 2, 26, 969, DateTimeKind.Utc).AddTicks(4881));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastLoginDate",
                table: "Users");
        }
    }
}
