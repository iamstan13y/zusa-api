using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZUSA.API.Migrations
{
    public partial class AccountsRefinedToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ZAccounts_Schools_SchoolId",
                table: "ZAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ZAccounts",
                table: "ZAccounts");

            migrationBuilder.RenameTable(
                name: "ZAccounts",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_ZAccounts_SchoolId",
                table: "Accounts",
                newName: "IX_Accounts_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Schools_SchoolId",
                table: "Accounts",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Schools_SchoolId",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "ZAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_SchoolId",
                table: "ZAccounts",
                newName: "IX_ZAccounts_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ZAccounts",
                table: "ZAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ZAccounts_Schools_SchoolId",
                table: "ZAccounts",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
