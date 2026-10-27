using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class GenreRatingOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "TimeToBeat",
                table: "Game",
                type: "int",
                maxLength: 32,
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AudienceRatingId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenreId",
                table: "Game",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AudienceRating",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudienceRatingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AudienceRating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AudienceRating_AudienceRating_AudienceRatingId",
                        column: x => x.AudienceRatingId,
                        principalTable: "AudienceRating",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Genre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Game_AudienceRatingId",
                table: "Game",
                column: "AudienceRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Game_GenreId",
                table: "Game",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceRating_AudienceRatingId",
                table: "AudienceRating",
                column: "AudienceRatingId");

            migrationBuilder.CreateIndex(
                name: "IX_Genre_GenreId",
                table: "Genre",
                column: "GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AudienceRating_AudienceRatingId",
                table: "Game",
                column: "AudienceRatingId",
                principalTable: "AudienceRating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_Genre_GenreId",
                table: "Game",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Game_AudienceRating_AudienceRatingId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_Genre_GenreId",
                table: "Game");

            migrationBuilder.DropTable(
                name: "AudienceRating");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropIndex(
                name: "IX_Game_AudienceRatingId",
                table: "Game");

            migrationBuilder.DropIndex(
                name: "IX_Game_GenreId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "AudienceRatingId",
                table: "Game");

            migrationBuilder.DropColumn(
                name: "GenreId",
                table: "Game");

            migrationBuilder.AlterColumn<int>(
                name: "TimeToBeat",
                table: "Game",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 32);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Game",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Rating",
                table: "Game",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }
    }
}
