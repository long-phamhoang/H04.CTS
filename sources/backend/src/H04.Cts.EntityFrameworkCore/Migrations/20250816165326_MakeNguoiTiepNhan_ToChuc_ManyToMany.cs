using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class MakeNguoiTiepNhan_ToChuc_ManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE \"AppNguoiTiepNhans\" DROP CONSTRAINT IF EXISTS \"FK_AppNguoiTiepNhans_AppToChucs_OrganizationId\";");
            migrationBuilder.Sql("DROP INDEX IF EXISTS \"IX_AppNguoiTiepNhans_OrganizationId\";");
            migrationBuilder.Sql("ALTER TABLE \"AppNguoiTiepNhans\" DROP COLUMN IF EXISTS \"OrganizationId\";");

            migrationBuilder.CreateTable(
                name: "AppOrganizationNguoiTiepNhans",
                columns: table => new
                {
                    OrganizationId = table.Column<long>(type: "bigint", nullable: false),
                    NguoiTiepNhanId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrganizationNguoiTiepNhans", x => new { x.OrganizationId, x.NguoiTiepNhanId });
                    table.ForeignKey(
                        name: "FK_AppOrganizationNguoiTiepNhan_NguoiTiepNhan_NguoiTiepNhanId",
                        column: x => x.NguoiTiepNhanId,
                        principalTable: "AppNguoiTiepNhans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrganizationNguoiTiepNhan_ToChuc_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "AppToChucs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationNguoiTiepNhans_NguoiTiepNhanId",
                table: "AppOrganizationNguoiTiepNhans",
                column: "NguoiTiepNhanId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationNguoiTiepNhans_OrganizationId",
                table: "AppOrganizationNguoiTiepNhans",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppOrganizationNguoiTiepNhans");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationId",
                table: "AppNguoiTiepNhans",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_OrganizationId",
                table: "AppNguoiTiepNhans",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppNguoiTiepNhans_AppToChucs_OrganizationId",
                table: "AppNguoiTiepNhans",
                column: "OrganizationId",
                principalTable: "AppToChucs",
                principalColumn: "Id");
        }
    }
}
