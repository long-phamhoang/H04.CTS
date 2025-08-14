using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace H04.Cts.Migrations
{
    /// <inheritdoc />
    public partial class AddTestModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppNoiCapCCCDs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Abbreviation = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Province = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNoiCapCCCDs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppNguoiTiepNhans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrganizationId = table.Column<long>(type: "bigint", nullable: true),
                    FullName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    CCCD = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NoiCapCCCDId = table.Column<long>(type: "bigint", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    SubmissionAddress = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Province = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Ward = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    IsDefault = table.Column<bool>(type: "boolean", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeletedBy = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNguoiTiepNhans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppNguoiTiepNhans_AppNoiCapCCCDs_NoiCapCCCDId",
                        column: x => x.NoiCapCCCDId,
                        principalTable: "AppNoiCapCCCDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppNguoiTiepNhans_AppToChucs_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "AppToChucs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_CCCD",
                table: "AppNguoiTiepNhans",
                column: "CCCD");

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_DateOfIssue",
                table: "AppNguoiTiepNhans",
                column: "DateOfIssue");

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_Email",
                table: "AppNguoiTiepNhans",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_FullName",
                table: "AppNguoiTiepNhans",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_NoiCapCCCDId",
                table: "AppNguoiTiepNhans",
                column: "NoiCapCCCDId");

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_Phone",
                table: "AppNguoiTiepNhans",
                column: "Phone");

            migrationBuilder.CreateIndex(
                name: "IX_AppNguoiTiepNhans_Position",
                table: "AppNguoiTiepNhans",
                column: "Position");

            migrationBuilder.CreateIndex(
                name: "UX_NguoiTiepNhans_DefaultPerOrg",
                table: "AppNguoiTiepNhans",
                column: "OrganizationId",
                unique: true,
                filter: "\"IsDefault\" = true AND \"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_AppNoiCapCCCDs_Abbreviation",
                table: "AppNoiCapCCCDs",
                column: "Abbreviation");

            migrationBuilder.CreateIndex(
                name: "IX_AppNoiCapCCCDs_Address",
                table: "AppNoiCapCCCDs",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_AppNoiCapCCCDs_Code",
                table: "AppNoiCapCCCDs",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_AppNoiCapCCCDs_IsActive",
                table: "AppNoiCapCCCDs",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_AppNoiCapCCCDs_Name",
                table: "AppNoiCapCCCDs",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_AppNoiCapCCCDs_Province",
                table: "AppNoiCapCCCDs",
                column: "Province");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppNguoiTiepNhans");

            migrationBuilder.DropTable(
                name: "AppNoiCapCCCDs");
        }
    }
}
