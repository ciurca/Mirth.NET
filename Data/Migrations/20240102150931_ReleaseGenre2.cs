using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMPD.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReleaseGenre2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreRelease");

            migrationBuilder.CreateTable(
                name: "ReleaseGenres",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseID = table.Column<int>(type: "int", nullable: false),
                    GenreID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReleaseGenres", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReleaseGenres_Genres_GenreID",
                        column: x => x.GenreID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReleaseGenres_Releases_ReleaseID",
                        column: x => x.ReleaseID,
                        principalTable: "Releases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseGenres_GenreID",
                table: "ReleaseGenres",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_ReleaseGenres_ReleaseID",
                table: "ReleaseGenres",
                column: "ReleaseID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReleaseGenres");

            migrationBuilder.CreateTable(
                name: "GenreRelease",
                columns: table => new
                {
                    GenresID = table.Column<int>(type: "int", nullable: false),
                    ReleasesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreRelease", x => new { x.GenresID, x.ReleasesID });
                    table.ForeignKey(
                        name: "FK_GenreRelease_Genres_GenresID",
                        column: x => x.GenresID,
                        principalTable: "Genres",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreRelease_Releases_ReleasesID",
                        column: x => x.ReleasesID,
                        principalTable: "Releases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreRelease_ReleasesID",
                table: "GenreRelease",
                column: "ReleasesID");
        }
    }
}
