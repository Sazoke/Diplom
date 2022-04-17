using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_TeacherId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_AspNetUsers_TeacherId",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_TeacherId",
                table: "Tests");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Tests",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Tests",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_TeacherId",
                table: "Tests",
                newName: "IX_Tests_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Tags",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Results",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Questions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Materials",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Materials",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_TeacherId",
                table: "Materials",
                newName: "IX_Materials_CreatedById");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Content",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "Activities",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Activities",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_TeacherId",
                table: "Activities",
                newName: "IX_Activities_CreatedById");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Tags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Results",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Content",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatedById",
                table: "Tags",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Results_CreatedById",
                table: "Results",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_CreatedById",
                table: "Questions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Content_CreatedById",
                table: "Content",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_CreatedById",
                table: "Activities",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Content_AspNetUsers_CreatedById",
                table: "Content",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_AspNetUsers_CreatedById",
                table: "Materials",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_AspNetUsers_CreatedById",
                table: "Questions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Results_AspNetUsers_CreatedById",
                table: "Results",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_CreatedById",
                table: "Tags",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_CreatedById",
                table: "Tests",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_AspNetUsers_CreatedById",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Content_AspNetUsers_CreatedById",
                table: "Content");

            migrationBuilder.DropForeignKey(
                name: "FK_Materials_AspNetUsers_CreatedById",
                table: "Materials");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_AspNetUsers_CreatedById",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Results_AspNetUsers_CreatedById",
                table: "Results");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_CreatedById",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tests_AspNetUsers_CreatedById",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CreatedById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Results_CreatedById",
                table: "Results");

            migrationBuilder.DropIndex(
                name: "IX_Questions_CreatedById",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Content_CreatedById",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Content");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Tests",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tests",
                newName: "Created");

            migrationBuilder.RenameIndex(
                name: "IX_Tests_CreatedById",
                table: "Tests",
                newName: "IX_Tests_TeacherId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Tags",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Results",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Questions",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Materials",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Materials",
                newName: "Created");

            migrationBuilder.RenameIndex(
                name: "IX_Materials_CreatedById",
                table: "Materials",
                newName: "IX_Materials_TeacherId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Content",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Activities",
                newName: "TeacherId");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Activities",
                newName: "Created");

            migrationBuilder.RenameIndex(
                name: "IX_Activities_CreatedById",
                table: "Activities",
                newName: "IX_Activities_TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_AspNetUsers_TeacherId",
                table: "Activities",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Materials_AspNetUsers_TeacherId",
                table: "Materials",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_AspNetUsers_TeacherId",
                table: "Tests",
                column: "TeacherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
