using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class addloaihosoloaictsentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLoaiCTSs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaLoaiCTS = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    TenLoaiCTS = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
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
                    table.PrimaryKey("PK_AppLoaiCTSs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppLoaiHoSos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaLoaiHoSo = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    TenLoaiHoSo = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
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
                    table.PrimaryKey("PK_AppLoaiHoSos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppLoaiCTSs_MaLoaiCTS",
                table: "AppLoaiCTSs",
                column: "MaLoaiCTS",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_AppLoaiCTSs_TrangThai",
                table: "AppLoaiCTSs",
                column: "TrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_AppLoaiHoSos_MaLoaiHoSo",
                table: "AppLoaiHoSos",
                column: "MaLoaiHoSo",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_AppLoaiHoSos_TrangThai",
                table: "AppLoaiHoSos",
                column: "TrangThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppLoaiCTSs");

            migrationBuilder.DropTable(
                name: "AppLoaiHoSos");
        }
    }
}
