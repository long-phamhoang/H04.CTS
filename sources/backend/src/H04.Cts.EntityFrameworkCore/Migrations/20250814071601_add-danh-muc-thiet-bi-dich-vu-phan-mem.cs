using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class adddanhmucthietbidichvuphanmem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppLoaiThietBiDichVuPhanMems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MaLoaiThietBiDichVuPhanMem = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TenLoaiThietBiDichVuPhanMem = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppLoaiThietBiDichVuPhanMems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppThietBiDichVuPhanMems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenCoQuanToChuc = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    TenThietBiDichVuPhanMem = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    LoaiThietBiDichVuPhanMemId = table.Column<long>(type: "bigint", nullable: false),
                    TinhThanhPhoId = table.Column<long>(type: "bigint", nullable: true),
                    DiaChiThuCongVu = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DiaChiIP = table.Column<string>(type: "text", nullable: true),
                    DNS = table.Column<string>(type: "text", nullable: true),
                    DiaChiIP1 = table.Column<string>(type: "text", nullable: true),
                    DNS1 = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_AppThietBiDichVuPhanMems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppThietBiDichVuPhanMems_AppLoaiThietBiDichVuPhanMems_LoaiT~",
                        column: x => x.LoaiThietBiDichVuPhanMemId,
                        principalTable: "AppLoaiThietBiDichVuPhanMems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppLoaiThietBiDichVuPhanMems_MaLoaiThietBiDichVuPhanMem",
                table: "AppLoaiThietBiDichVuPhanMems",
                column: "MaLoaiThietBiDichVuPhanMem",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_AppLoaiThietBiDichVuPhanMems_TenLoaiThietBiDichVuPhanMem",
                table: "AppLoaiThietBiDichVuPhanMems",
                column: "TenLoaiThietBiDichVuPhanMem");

            migrationBuilder.CreateIndex(
                name: "IX_AppLoaiThietBiDichVuPhanMems_TrangThai",
                table: "AppLoaiThietBiDichVuPhanMems",
                column: "TrangThai");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_DiaChiIP",
                table: "AppThietBiDichVuPhanMems",
                column: "DiaChiIP");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_DiaChiIP1",
                table: "AppThietBiDichVuPhanMems",
                column: "DiaChiIP1");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_DiaChiThuCongVu",
                table: "AppThietBiDichVuPhanMems",
                column: "DiaChiThuCongVu");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_DNS",
                table: "AppThietBiDichVuPhanMems",
                column: "DNS");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_DNS1",
                table: "AppThietBiDichVuPhanMems",
                column: "DNS1");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_LoaiThietBiDichVuPhanMemId",
                table: "AppThietBiDichVuPhanMems",
                column: "LoaiThietBiDichVuPhanMemId");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_TenCoQuanToChuc",
                table: "AppThietBiDichVuPhanMems",
                column: "TenCoQuanToChuc");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_TenThietBiDichVuPhanMem",
                table: "AppThietBiDichVuPhanMems",
                column: "TenThietBiDichVuPhanMem");

            migrationBuilder.CreateIndex(
                name: "IX_AppThietBiDichVuPhanMems_TinhThanhPhoId",
                table: "AppThietBiDichVuPhanMems",
                column: "TinhThanhPhoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppThietBiDichVuPhanMems");

            migrationBuilder.DropTable(
                name: "AppLoaiThietBiDichVuPhanMems");
        }
    }
}
