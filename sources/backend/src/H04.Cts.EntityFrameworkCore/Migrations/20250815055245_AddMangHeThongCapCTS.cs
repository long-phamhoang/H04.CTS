using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class AddMangHeThongCapCTS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMangCTSs",
                table: "AppMangCTSs");

            migrationBuilder.RenameTable(
                name: "AppMangCTSs",
                newName: "AppMangHeThongCapCTSs");

            migrationBuilder.RenameIndex(
                name: "IX_AppMangCTSs_MaMangCTS",
                table: "AppMangHeThongCapCTSs",
                newName: "IX_AppMangHeThongCapCTSs_MaMangCTS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMangHeThongCapCTSs",
                table: "AppMangHeThongCapCTSs",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppMangHeThongCapCTSs",
                table: "AppMangHeThongCapCTSs");

            migrationBuilder.RenameTable(
                name: "AppMangHeThongCapCTSs",
                newName: "AppMangCTSs");

            migrationBuilder.RenameIndex(
                name: "IX_AppMangHeThongCapCTSs_MaMangCTS",
                table: "AppMangCTSs",
                newName: "IX_AppMangCTSs_MaMangCTS");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppMangCTSs",
                table: "AppMangCTSs",
                column: "Id");
        }
    }
}
