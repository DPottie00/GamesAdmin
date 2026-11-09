using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamesAdmin.Migrations
{
    /// <inheritdoc />
    public partial class AddAudienceRatingOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CriticReview",
                type: "nvarchar(2056)",
                maxLength: 2056,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "AudienceRating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                UPDATE AudienceRating
                SET [Order] = CASE Title
                    WHEN 'E' THEN 1
                    WHEN 'E10' THEN 2
                    WHEN 'T' THEN 3
                    WHEN 'M' THEN 4
                    WHEN 'A' THEN 5
                    WHEN 'RP' THEN 10
                    WHEN 'RP17' THEN 11
                END;
            ");

            migrationBuilder.CreateIndex(
                name: "IX_AudienceRating_Order",
                table: "AudienceRating",
                column: "Order",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AudienceRating_Order",
                table: "AudienceRating");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "AudienceRating");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CriticReview",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(2056)",
                oldMaxLength: 2056);
        }
    }
}
