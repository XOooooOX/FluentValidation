using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Models.Context.Migrations
{
    public partial class ChangeFirstNameColumne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName_Value",
                table: "Student",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Student",
                newName: "FirstName_Value");
        }
    }
}
