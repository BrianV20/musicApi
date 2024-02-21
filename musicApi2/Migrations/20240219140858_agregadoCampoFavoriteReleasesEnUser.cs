using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicApi2.Migrations
{
    /// <inheritdoc />
    public partial class agregadoCampoFavoriteReleasesEnUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavoriteReleases",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavoriteReleases",
                table: "Users");
        }
    }
}
