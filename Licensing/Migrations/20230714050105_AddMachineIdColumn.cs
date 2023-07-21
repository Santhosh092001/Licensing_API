using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licensing.Migrations
{
    public partial class AddMachineIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MachineId",
                table: "ActivationKeys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "ActivationKeys");
        }
    }
}
