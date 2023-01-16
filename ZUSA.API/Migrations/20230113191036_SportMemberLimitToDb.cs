using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZUSA.API.Migrations
{
    public partial class SportMemberLimitToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamMemberLimit",
                table: "Sports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamMemberLimit",
                table: "Sports");
        }
    }
}
