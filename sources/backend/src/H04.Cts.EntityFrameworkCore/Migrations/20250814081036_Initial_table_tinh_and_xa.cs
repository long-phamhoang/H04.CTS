using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class Initial_table_tinh_and_xa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTinhThanhPhos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaTinhThanhPho = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TenTinhThanhPho = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTinhThanhPhos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppXaPhuongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaXaPhuong = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TenXaPhuong = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    TinhThanhPhoId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppXaPhuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppXaPhuongs_AppTinhThanhPhos_TinhThanhPhoId",
                        column: x => x.TinhThanhPhoId,
                        principalTable: "AppTinhThanhPhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTinhThanhPhos_MaTinhThanhPho",
                table: "AppTinhThanhPhos",
                column: "MaTinhThanhPho");

            migrationBuilder.CreateIndex(
                name: "IX_AppTinhThanhPhos_TenTinhThanhPho",
                table: "AppTinhThanhPhos",
                column: "TenTinhThanhPho");

            migrationBuilder.CreateIndex(
                name: "IX_AppTinhThanhPhos_TrangThai",
                table: "AppTinhThanhPhos",
                column: "TrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_AppXaPhuongs_MaXaPhuong",
                table: "AppXaPhuongs",
                column: "MaXaPhuong");

            migrationBuilder.CreateIndex(
                name: "IX_AppXaPhuongs_TenXaPhuong",
                table: "AppXaPhuongs",
                column: "TenXaPhuong");

            migrationBuilder.CreateIndex(
                name: "IX_AppXaPhuongs_TinhThanhPhoId",
                table: "AppXaPhuongs",
                column: "TinhThanhPhoId");

            migrationBuilder.CreateIndex(
                name: "IX_AppXaPhuongs_TrangThai",
                table: "AppXaPhuongs",
                column: "TrangThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppXaPhuongs");

            migrationBuilder.DropTable(
                name: "AppTinhThanhPhos");
        }
    }
}
