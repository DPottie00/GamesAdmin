using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddDevAndPlat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PlatformId",
                table: "Game",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Developer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platform",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform", x => x.Id);
                });

            //Take each unique developer, And create a "Developer"
            migrationBuilder.Sql(@"
                INSERT INTO Developer (Name)
                SELECT DISTINCT Developer
                FROM Game;
            ");
            //Take each unique platform, And create a "Platform"
            migrationBuilder.Sql(@"
                INSERT INTO Platform (Name)
                SELECT DISTINCT Platform
                FROM Game;
            ");
            //Match each games developer to the new Table, assign its ID
            migrationBuilder.Sql(@"
                UPDATE Game
                SET DeveloperId = Developer.Id
                FROM Game
                INNER JOIN Developer ON Game.Developer = Developer.Name;
            ");
            //Match each games platform to the new Table, assign its ID
            migrationBuilder.Sql(@"
                UPDATE Game
                SET PlatformId = Platform.Id
                FROM Game
                INNER JOIN Platform ON Game.Platform = Platform.Name;
            ");

            //Now that's things are transferred, no longer nullable
            migrationBuilder.AlterColumn<int>(
                name: "DeveloperId",
                table: "Game",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlatformId",
                table: "Game",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            //Drop old Columns
            migrationBuilder.DropColumn(
                name: "Developer",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Game");

            migrationBuilder.CreateIndex(
                name: "IX_Game_DeveloperId",
                table: "Game",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_PlatformId",
                table: "Game",
                column: "PlatformId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game",
                column: "DeveloperId",
                principalTable: "Developer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Platform_PlatformId",
                table: "Game",
                column: "PlatformId",
                principalTable: "Platform",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_Developer_DeveloperId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Platform_PlatformId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_DeveloperId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_PlatformId",
                table: "Game");

            migrationBuilder.AddColumn<string>(
                name: "Developer",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Game",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
                UPDATE Game
                SET Developer = Developer.Name
                FROM Game
                INNER JOIN Developer ON Game.DeveloperId = Developer.Id;
            ");

            migrationBuilder.Sql(@"
                UPDATE Game
                SET Platform = Platform.Name
                FROM Game
                INNER JOIN Platform ON Game.PlatformId = Platform.Id;
            ");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "PlatformId",
                table: "Game");
             
            migrationBuilder.DropTable(
                            name: "Developer");

            migrationBuilder.DropTable(
                name: "Platform");
        }
    }
}
