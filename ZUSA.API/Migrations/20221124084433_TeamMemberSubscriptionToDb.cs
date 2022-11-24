using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZUSA.API.Migrations
{
    public partial class TeamMemberSubscriptionToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Schools_SchoolId",
                table: "TeamMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Sports_SportId",
                table: "TeamMembers");

            migrationBuilder.DropIndex(
                name: "IX_TeamMembers_SchoolId",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "TeamMembers");

            migrationBuilder.DropColumn(
                name: "SchoolId",
                table: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "SportId",
                table: "TeamMembers",
                newName: "SubscriptionId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_SportId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Subscriptions_SubscriptionId",
                table: "TeamMembers",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamMembers_Subscriptions_SubscriptionId",
                table: "TeamMembers");

            migrationBuilder.RenameColumn(
                name: "SubscriptionId",
                table: "TeamMembers",
                newName: "SportId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamMembers_SubscriptionId",
                table: "TeamMembers",
                newName: "IX_TeamMembers_SportId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "TeamMembers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SchoolId",
                table: "TeamMembers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TeamMembers_SchoolId",
                table: "TeamMembers",
                column: "SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Schools_SchoolId",
                table: "TeamMembers",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamMembers_Sports_SportId",
                table: "TeamMembers",
                column: "SportId",
                principalTable: "Sports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
