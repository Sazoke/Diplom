using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MaterialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_SchoolAreas_SchoolAreaId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Files",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Results",
                newName: "Username");

            migrationBuilder.AlterColumn<long>(
                name: "SchoolAreaId",
                table: "Tags",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Content",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsFile = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Content", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Content_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Content_MaterialId",
                table: "Content",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_SchoolAreas_SchoolAreaId",
                table: "Tags",
                column: "SchoolAreaId",
                principalTable: "SchoolAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_SchoolAreas_SchoolAreaId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "Content");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Results",
                newName: "UserName");

            migrationBuilder.AddColumn<List<string>>(
                name: "Files",
                table: "Tests",
                type: "text[]",
                nullable: false);

            migrationBuilder.AlterColumn<long>(
                name: "SchoolAreaId",
                table: "Tags",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<List<string>>(
                name: "Files",
                table: "Materials",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddColumn<List<string>>(
                name: "Files",
                table: "Activities",
                type: "text[]",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_SchoolAreas_SchoolAreaId",
                table: "Tags",
                column: "SchoolAreaId",
                principalTable: "SchoolAreas",
                principalColumn: "Id");
        }
    }
}
