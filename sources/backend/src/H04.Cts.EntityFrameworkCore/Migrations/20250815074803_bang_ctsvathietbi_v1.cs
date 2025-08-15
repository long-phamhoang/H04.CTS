using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class bang_ctsvathietbi_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCTSVaThietBis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenCTS = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    SoHieuCTS = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    DiaChiThuDienTuCongVu = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    NgayHieuLuc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NgayHetHan = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CoQuanToChuc = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    LoaiCTS = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    HoSo = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    VanBan = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    HoSoThuHoi = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    VanBanThuHoi = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Notes = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    CnTen = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CnNgaySinh = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CnSDT = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    CnCCCD = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    CnNgayCap = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CnNoiCap = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    CnChucVu = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    CnDiaChiThuDienTuCongVu = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    CnTinhTP = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TcTen = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    TcMaDinhDanh = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    TcMaSoThue = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    TcDiaChiThuDienTuCongVu = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    TcSDT = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TcTinhTP = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TcPhuongXa = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TcMaQuanHeNganSach = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
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
                    table.PrimaryKey("PK_AppCTSVaThietBis", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCTSVaThietBis_SoHieuCTS",
                table: "AppCTSVaThietBis",
                column: "SoHieuCTS",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_AppCTSVaThietBis_TenCTS_CoQuanToChuc_TrangThai",
                table: "AppCTSVaThietBis",
                columns: new[] { "TenCTS", "CoQuanToChuc", "TrangThai" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCTSVaThietBis");
        }
    }
}
