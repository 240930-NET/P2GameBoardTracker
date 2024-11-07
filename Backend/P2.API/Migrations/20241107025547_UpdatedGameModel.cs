using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGameModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2024, 11, 7, 2, 55, 45, 586, DateTimeKind.Utc).AddTicks(8238));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "LastLoginDate",
                value: new DateTime(2024, 11, 4, 5, 2, 26, 969, DateTimeKind.Utc).AddTicks(4881));
        }
    }
}
