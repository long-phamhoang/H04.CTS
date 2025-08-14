using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class AddTestModel3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_NguoiTiepNhans_DefaultPerOrg",
                table: "AppNguoiTiepNhans");

            migrationBuilder.AlterColumn<long>(
                name: "NoiCapCCCDId",
                table: "AppNguoiTiepNhans",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_OrganizationId",
                table: "AppNguoiTiepNhans",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppNguoiTiepNhans_OrganizationId",
                table: "AppNguoiTiepNhans");

            migrationBuilder.AlterColumn<long>(
                name: "NoiCapCCCDId",
                table: "AppNguoiTiepNhans",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UX_NguoiTiepNhans_DefaultPerOrg",
                table: "AppNguoiTiepNhans",
                column: "OrganizationId",
                unique: true,
                filter: "\"IsDefault\" = true AND \"IsDeleted\" = false");
        }
    }
}
