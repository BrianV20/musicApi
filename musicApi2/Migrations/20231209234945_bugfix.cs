using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace musicApi2.Migrations
{
    /// <inheritdoc />
    public partial class bugfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReleasesGenres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleasesGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReleasesGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleasesGenres_Releases_ReleaseId",
                        column: x => x.ReleaseId,
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReleasesGenres_GenreId",
                table: "ReleasesGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_ReleasesGenres_ReleaseId",
                table: "ReleasesGenres",
                column: "ReleaseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "ReleasesGenres");
            migrationBuilder.DropTable(name: "Genres");
            migrationBuilder.DropTable(name: "Releases");
            migrationBuilder.DropTable(name: "Artists");
            migrationBuilder.DropTable(name: "Reviews");
            migrationBuilder.DropTable(name: "Ratings");
            migrationBuilder.DropTable(name: "Users");
            migrationBuilder.DropTable(name: "ArtistsReleases");
        }
    }
}
