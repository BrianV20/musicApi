using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicApi2.Migrations
{
    /// <inheritdoc />
    public partial class agregadaPropiedadGenresARelease : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Releases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Releases");
        }
    }
}
