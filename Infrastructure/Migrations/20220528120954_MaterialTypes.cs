using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MaterialTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Results_Tests_TestId",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Materials");

            migrationBuilder.AlterColumn<long>(
                name: "TestId",
                table: "Results",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TypeId",
                table: "Materials",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SingleTypeName = table.Column<string>(type: "text", nullable: false),
                    MultipleTypeName = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTypes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Materials_TypeId",
                table: "Materials",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTypes_CreatedById",
                table: "MaterialTypes",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_MaterialTypes_TypeId",
                table: "Materials",
                column: "TypeId",
                principalTable: "MaterialTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Tests_TestId",
                table: "Results",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Materials_MaterialTypes_TypeId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_Tests_TestId",
                table: "Results");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropIndex(
                name: "IX_Materials_TypeId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Materials");

            migrationBuilder.AlterColumn<long>(
                name: "TestId",
                table: "Results",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Materials",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Results_Tests_TestId",
                table: "Results",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
