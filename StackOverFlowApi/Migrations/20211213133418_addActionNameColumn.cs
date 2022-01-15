using Microsoft.EntityFrameworkCore.Migrations;

namespace StackOverFlowApi.Migrations
{
    public partial class addActionNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActionName",
                table: "Actions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionName",
                table: "Actions");
        }
    }
}
