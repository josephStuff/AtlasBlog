using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtlasBlog.Data.Migrations
{
    public partial class _pagation1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResearchTopic",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResearchTopic",
                table: "Blogs");
        }
    }
}
