using Microsoft.EntityFrameworkCore.Migrations;

namespace LT.SO.Infra.CrossCutting.Identity.Migrations
{
    public partial class User_First_Pass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FirstPass",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPass",
                table: "AspNetUsers");
        }
    }
}
