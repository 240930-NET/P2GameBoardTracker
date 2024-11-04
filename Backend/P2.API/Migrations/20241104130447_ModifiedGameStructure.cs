using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace P2.API.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedGameStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

             migrationBuilder.DropTable(name: "Games");

        // Recreate the Games table with the desired schema
        migrationBuilder.CreateTable(
            name: "Games",
            columns: table => new
            {
                GameId = table.Column<int>(nullable: false), // or your desired type
                Name = table.Column<string>(nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Rating = table.Column<double>(nullable: false, defaultValue: 0.0)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Games", x => x.GameId);
            });

        // Optionally, insert new sample data
        migrationBuilder.InsertData(
            table: "Games",
            columns: new[] { "GameId", "Name", "Description", "Rating" },
            values: new object[,]
            {
                { 1, "Sample Fake game", "", 0.0 },
                { 2, "Sample Fake Game 2", "Investigating a letter from his late wife, James returns to where they made so many memories - Silent Hill.", 0.0 }
            });
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(name: "Games");

        // Recreate the old table with the previous schema
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                });
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "Description", "Name" },
                values: new object[,]
                {
                        { 1, "", "Counter-Strike 2" },
                        { 2, "Investigating a letter from his late wife, James returns to where they made so many memories - Silent Hill.", "Silent Hill 2" }
                });
         }
    }
}
