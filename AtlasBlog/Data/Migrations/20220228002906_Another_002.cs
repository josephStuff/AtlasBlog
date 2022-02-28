using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtlasBlog.Data.Migrations
{
    public partial class Another_002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Blogs",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "ImageExt",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ImageExt",
                table: "Blogs");
        }
    }
}
