using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMPD.Data.Migrations
{
    /// <inheritdoc />
    public partial class LibraryRenameToMusicLibrary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibraryRelease");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.CreateTable(
                name: "MusicLibraries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicLibraries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MusicLibraries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MusicLibraryRelease",
                columns: table => new
                {
                    MusicLibrariesID = table.Column<int>(type: "int", nullable: false),
                    ReleasesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicLibraryRelease", x => new { x.MusicLibrariesID, x.ReleasesID });
                    table.ForeignKey(
                        name: "FK_MusicLibraryRelease_MusicLibraries_MusicLibrariesID",
                        column: x => x.MusicLibrariesID,
                        principalTable: "MusicLibraries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicLibraryRelease_Releases_ReleasesID",
                        column: x => x.ReleasesID,
                        principalTable: "Releases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MusicLibraries_UserId",
                table: "MusicLibraries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicLibraryRelease_ReleasesID",
                table: "MusicLibraryRelease",
                column: "ReleasesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicLibraryRelease");

            migrationBuilder.DropTable(
                name: "MusicLibraries");

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Libraries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibraryRelease",
                columns: table => new
                {
                    LibrariesID = table.Column<int>(type: "int", nullable: false),
                    ReleasesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryRelease", x => new { x.LibrariesID, x.ReleasesID });
                    table.ForeignKey(
                        name: "FK_LibraryRelease_Libraries_LibrariesID",
                        column: x => x.LibrariesID,
                        principalTable: "Libraries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryRelease_Releases_ReleasesID",
                        column: x => x.ReleasesID,
                        principalTable: "Releases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libraries_UserId",
                table: "Libraries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryRelease_ReleasesID",
                table: "LibraryRelease",
                column: "ReleasesID");
        }
    }
}
