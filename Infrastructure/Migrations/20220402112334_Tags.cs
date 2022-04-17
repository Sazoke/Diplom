using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AreaId",
                table: "Tests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AreaId",
                table: "Materials",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "AreaId",
                table: "Activities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "SchoolAreas",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolAreas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ActivityId = table.Column<long>(type: "bigint", nullable: true),
                    MaterialId = table.Column<long>(type: "bigint", nullable: true),
                    SchoolAreaId = table.Column<long>(type: "bigint", nullable: true),
                    TestId = table.Column<long>(type: "bigint", nullable: true),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_SchoolAreas_SchoolAreaId",
                        column: x => x.SchoolAreaId,
                        principalTable: "SchoolAreas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tags_Tests_TestId",
                        column: x => x.TestId,
                        principalTable: "Tests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_AreaId",
                table: "Tests",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_AreaId",
                table: "Materials",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_AreaId",
                table: "Activities",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ActivityId",
                table: "Tags",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MaterialId",
                table: "Tags",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SchoolAreaId",
                table: "Tags",
                column: "SchoolAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TestId",
                table: "Tags",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_SchoolAreas_AreaId",
                table: "Activities",
                column: "AreaId",
                principalTable: "SchoolAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_SchoolAreas_AreaId",
                table: "Materials",
                column: "AreaId",
                principalTable: "SchoolAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_SchoolAreas_AreaId",
                table: "Tests",
                column: "AreaId",
                principalTable: "SchoolAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_SchoolAreas_AreaId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_SchoolAreas_AreaId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_SchoolAreas_AreaId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "SchoolAreas");

            migrationBuilder.DropIndex(
                name: "IX_Tests_AreaId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Materials_AreaId",
                table: "Materials");

            migrationBuilder.DropIndex(
                name: "IX_Activities_AreaId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Activities");
        }
    }
}
