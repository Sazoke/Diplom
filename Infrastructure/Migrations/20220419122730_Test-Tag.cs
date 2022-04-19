using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class TestTag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Tags_TagId",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TagId",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TagId",
                table: "Tests");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TagId",
                table: "Tests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TagId",
                table: "Tests",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Tags_TagId",
                table: "Tests",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id");
        }
    }
}
