using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class TagUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Activities_ActivityId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Materials_MaterialId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Tests_TestId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_ActivityId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_MaterialId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TestId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TestId",
                table: "Tags");

            migrationBuilder.CreateTable(
                name: "ActivityTag",
                columns: table => new
                {
                    ActivitiesId = table.Column<long>(type: "bigint", nullable: false),
                    TagsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTag", x => new { x.ActivitiesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ActivityTag_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTag",
                columns: table => new
                {
                    MaterialsId = table.Column<long>(type: "bigint", nullable: false),
                    TagsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTag", x => new { x.MaterialsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_MaterialTag_Materials_MaterialsId",
                        column: x => x.MaterialsId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_ActivityTag_TagsId",
                table: "ActivityTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTag_TagsId",
                table: "MaterialTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_TagTest_TestsId",
                table: "TagTest",
                column: "TestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityTag");

            migrationBuilder.DropTable(
                name: "MaterialTag");

            migrationBuilder.DropTable(
                name: "TagTest");

            migrationBuilder.AddColumn<long>(
                name: "ActivityId",
                table: "Tags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MaterialId",
                table: "Tags",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "TestId",
                table: "Tags",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ActivityId",
                table: "Tags",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_MaterialId",
                table: "Tags",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TestId",
                table: "Tags",
                column: "TestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Activities_ActivityId",
                table: "Tags",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Materials_MaterialId",
                table: "Tags",
                column: "MaterialId",
                principalTable: "Materials",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Tests_TestId",
                table: "Tags",
                column: "TestId",
                principalTable: "Tests",
                principalColumn: "Id");
        }
    }
}
