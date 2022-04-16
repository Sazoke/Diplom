using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class SchoolAreaUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "SchoolAreas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SchoolAreas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_SchoolAreas_CreatedById",
                table: "SchoolAreas",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SchoolAreas_AspNetUsers_CreatedById",
                table: "SchoolAreas",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SchoolAreas_AspNetUsers_CreatedById",
                table: "SchoolAreas");

            migrationBuilder.DropIndex(
                name: "IX_SchoolAreas_CreatedById",
                table: "SchoolAreas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "SchoolAreas");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SchoolAreas");
        }
    }
}
