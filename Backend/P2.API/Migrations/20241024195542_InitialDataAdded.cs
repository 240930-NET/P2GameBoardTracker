using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace P2.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Backlogs");

            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "Backlogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Backlogs",
                columns: new[] { "GameId", "UserId", "Completed", "CompletionDate" },
                values: new object[] { 1, 1, false, null });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "", "Counter-Strike 2" },
                    { 2, "Investigating a letter from his late wife, James returns to where they made so many memories - Silent Hill.", "Silent Hill 2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Password", "UserName" },
                values: new object[] { 1, "Password", "Alfredo" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Backlogs",
                keyColumns: new[] { "GameId", "UserId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "GameId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Completed",
                table: "Backlogs");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Backlogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
