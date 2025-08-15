using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class bang_cts_va_thiet_bi_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppCtsVaThietBis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenCts = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    SoHieuCts = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    DiaChiThuDienTuCongVu = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    NgayHieuLuc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    NgayHetHan = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ToChucId = table.Column<long>(type: "bigint", nullable: true),
                    LoaiCts = table.Column<int>(type: "integer", nullable: true),
                    TrangThai = table.Column<int>(type: "integer", nullable: true),
                    HoSo = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    VanBan = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    HoSoThuHoi = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    VanBanThuHoi = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    Notes = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    Ten = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    NgaySinh = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SoDienThoai = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    MaDinhDanh = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    NgayCap = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NoiCapId = table.Column<long>(type: "bigint", nullable: false),
                    ChucVuId = table.Column<long>(type: "bigint", nullable: true),
                    MaSoThue = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: true),
                    TinhThanhPhoId = table.Column<long>(type: "bigint", nullable: false),
                    PhuongXaId = table.Column<long>(type: "bigint", nullable: true),
                    MaQuanHeNganSach = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
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
                    table.PrimaryKey("PK_AppCtsVaThietBis", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppCtsVaThietBis_SoHieuCts",
                table: "AppCtsVaThietBis",
                column: "SoHieuCts",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_AppCtsVaThietBis_TenCts_ToChucId_TrangThai",
                table: "AppCtsVaThietBis",
                columns: new[] { "TenCts", "ToChucId", "TrangThai" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppCtsVaThietBis");
        }
    }
}
