using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtlasBlog.Migrations
{
    public partial class _0003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Blogs_BlogId",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogId",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<string>(
                name: "BlogId",
                table: "BlogPosts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "BlogId1",
                table: "BlogPosts",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogId1",
                table: "BlogPosts",
                column: "BlogId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Blogs_BlogId1",
                table: "BlogPosts",
                column: "BlogId1",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogPosts_Blogs_BlogId1",
                table: "BlogPosts");

            migrationBuilder.DropIndex(
                name: "IX_BlogPosts_BlogId1",
                table: "BlogPosts");

            migrationBuilder.DropColumn(
                name: "BlogId1",
                table: "BlogPosts");

            migrationBuilder.AlterColumn<int>(
                name: "BlogId",
                table: "BlogPosts",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_BlogPosts_BlogId",
                table: "BlogPosts",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogPosts_Blogs_BlogId",
                table: "BlogPosts",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
