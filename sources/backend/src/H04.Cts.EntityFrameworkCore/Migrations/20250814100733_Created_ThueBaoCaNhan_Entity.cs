using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class Created_ThueBaoCaNhan_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppThueBaoCaNhans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HoTen = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    SoDinhDanhCaNhan = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NoiCap = table.Column<string>(type: "text", nullable: false),
                    NgayCap = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ToChucId = table.Column<long>(type: "bigint", nullable: false),
                    ChucVuId = table.Column<long>(type: "bigint", nullable: false),
                    DiaChiThuDienTuCongVu = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    TinhThanhPho = table.Column<long>(type: "bigint", nullable: true),
                    PhuongXa = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_AppThueBaoCaNhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppThueBaoCaNhans_AppChucVus_ChucVuId",
                        column: x => x.ChucVuId,
                        principalTable: "AppChucVus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppThueBaoCaNhans_AppToChucs_ToChucId",
                        column: x => x.ToChucId,
                        principalTable: "AppToChucs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppThueBaoCaNhans_ChucVuId",
                table: "AppThueBaoCaNhans",
                column: "ChucVuId");

            migrationBuilder.CreateIndex(
                name: "IX_AppThueBaoCaNhans_PhuongXa",
                table: "AppThueBaoCaNhans",
                column: "PhuongXa");

            migrationBuilder.CreateIndex(
                name: "IX_AppThueBaoCaNhans_SoDinhDanhCaNhan",
                table: "AppThueBaoCaNhans",
                column: "SoDinhDanhCaNhan",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppThueBaoCaNhans_TinhThanhPho",
                table: "AppThueBaoCaNhans",
                column: "TinhThanhPho");

            migrationBuilder.CreateIndex(
                name: "IX_AppThueBaoCaNhans_ToChucId",
                table: "AppThueBaoCaNhans",
                column: "ToChucId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppThueBaoCaNhans");
        }
    }
}
