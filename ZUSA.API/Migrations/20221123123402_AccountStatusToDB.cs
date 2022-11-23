using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZUSA.API.Migrations
{
    public partial class AccountStatusToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ZAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ZAccounts");
        }
    }
}
