using Microsoft.EntityFrameworkCore.Migrations;

namespace OKRs.Migrations
{
    public partial class AddCompletionRateToKeyResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CompletionRate",
                table: "KeyResults",
                type: "decimal(3,2)",
                nullable: false,
                defaultValue: 0.00m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompletionRate",
                table: "KeyResults");
        }
    }
}