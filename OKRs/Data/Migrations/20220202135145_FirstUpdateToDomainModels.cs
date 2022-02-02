using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OKRs.Migrations
{
    public partial class FirstUpdateToDomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KeyResults_Objectives_ObjectiveId",
                table: "KeyResults");

            migrationBuilder.DropIndex(
                name: "IX_KeyResults_ObjectiveId",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "ObjectiveId",
                table: "KeyResults");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Objectives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyResults",
                table: "Objectives",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "KeyResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeyResults",
                table: "Objectives");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Objectives",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "KeyResults",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "ObjectiveId",
                table: "KeyResults",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KeyResults_ObjectiveId",
                table: "KeyResults",
                column: "ObjectiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_KeyResults_Objectives_ObjectiveId",
                table: "KeyResults",
                column: "ObjectiveId",
                principalTable: "Objectives",
                principalColumn: "Id");
        }
    }
}
