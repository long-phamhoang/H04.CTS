using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class Add_DK_CTS_LucLuong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppDK_CTS_LucLuongs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenDieuKien = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    MaDieuKien = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true),
                    TrangThai = table.Column<int>(type: "integer", nullable: false),
                    GhiChu = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    LucLuongId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_AppDK_CTS_LucLuongs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppDK_CTS_LucLuongs_AppLucLuongs_LucLuongId",
                        column: x => x.LucLuongId,
                        principalTable: "AppLucLuongs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppDK_CTS_LucLuongs_LucLuongId",
                table: "AppDK_CTS_LucLuongs",
                column: "LucLuongId");

            migrationBuilder.CreateIndex(
                name: "IX_AppDK_CTS_LucLuongs_MaDieuKien",
                table: "AppDK_CTS_LucLuongs",
                column: "MaDieuKien",
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_AppDK_CTS_LucLuongs_TrangThai",
                table: "AppDK_CTS_LucLuongs",
                column: "TrangThai");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppDK_CTS_LucLuongs");
        }
    }
}
