using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marval.Domain.Migrations
{
    public partial class changedLastnameToSurname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Employees",
                newName: "Surname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Employees",
                newName: "LastName");
        }
    }
}
