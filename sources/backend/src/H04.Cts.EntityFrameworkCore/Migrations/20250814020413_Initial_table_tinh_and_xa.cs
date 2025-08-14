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
                name: "Tinhs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaTinh = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    TenTinh = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    SoThuTu = table.Column<int>(type: "integer", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tinhs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Xas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MaXa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    TenXa = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SoThuTu = table.Column<int>(type: "integer", nullable: true),
                    MoTa = table.Column<string>(type: "text", nullable: true),
                    TinhId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Xas_Tinhs_TinhId",
                        column: x => x.TinhId,
                        principalTable: "Tinhs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Xas_TinhId",
                table: "Xas",
                column: "TinhId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Xas");

            migrationBuilder.DropTable(
                name: "Tinhs");
        }
    }
}
