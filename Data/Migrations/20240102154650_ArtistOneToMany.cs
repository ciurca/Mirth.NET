using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProiectMPD.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArtistOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistRelease");

            migrationBuilder.AddColumn<int>(
                name: "ArtistID",
                table: "Releases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Releases_ArtistID",
                table: "Releases",
                column: "ArtistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Releases_Artists_ArtistID",
                table: "Releases",
                column: "ArtistID",
                principalTable: "Artists",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Releases_Artists_ArtistID",
                table: "Releases");

            migrationBuilder.DropIndex(
                name: "IX_Releases_ArtistID",
                table: "Releases");

            migrationBuilder.DropColumn(
                name: "ArtistID",
                table: "Releases");

            migrationBuilder.CreateTable(
                name: "ArtistRelease",
                columns: table => new
                {
                    ArtistsID = table.Column<int>(type: "int", nullable: false),
                    ReleasesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistRelease", x => new { x.ArtistsID, x.ReleasesID });
                    table.ForeignKey(
                        name: "FK_ArtistRelease_Artists_ArtistsID",
                        column: x => x.ArtistsID,
                        principalTable: "Artists",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistRelease_Releases_ReleasesID",
                        column: x => x.ReleasesID,
                        principalTable: "Releases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArtistRelease_ReleasesID",
                table: "ArtistRelease",
                column: "ReleasesID");
        }
    }
}
