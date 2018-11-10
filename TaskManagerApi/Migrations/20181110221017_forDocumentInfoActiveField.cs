using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManagerApi.Migrations
{
    public partial class forDocumentInfoActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "DocumentInfos",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "DocumentInfos");
        }
    }
}
