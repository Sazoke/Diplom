using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class MaterialBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_SchoolAreas_AreaId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "TagTest");

            migrationBuilder.DropIndex(
                name: "IX_Tests_AreaId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Tests");

            migrationBuilder.AddColumn<long>(
                name: "TagId",
                table: "Tests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EducationalMaterials",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Image = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalMaterials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalMaterials_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TagId",
                table: "Tests",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalMaterials_CreatedById",
                table: "EducationalMaterials",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Tags_TagId",
                table: "Tests",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Tags_TagId",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "EducationalMaterials");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TagId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tests");

            migrationBuilder.AddColumn<long>(
                name: "AreaId",
                table: "Tests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Tests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Tests",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TagTest",
                columns: table => new
                {
                    TagsId = table.Column<long>(type: "bigint", nullable: false),
                    TestsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTest", x => new { x.TagsId, x.TestsId });
                    table.ForeignKey(
                        name: "FK_TagTest_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTest_Tests_TestsId",
                        column: x => x.TestsId,
                        principalTable: "Tests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_AreaId",
                table: "Tests",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTest_TestsId",
                table: "TagTest",
                column: "TestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_SchoolAreas_AreaId",
                table: "Tests",
                column: "AreaId",
                principalTable: "SchoolAreas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
