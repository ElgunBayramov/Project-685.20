using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.WebUI.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Professions_ProfessionId",
                table: "Registers");

            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Statuses_StatusId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_ProfessionId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_StatusId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "UserStatusId",
                table: "Registers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "Registers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Registers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserStatusId",
                table: "Registers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Registers_ProfessionId",
                table: "Registers",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Registers_StatusId",
                table: "Registers",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Professions_ProfessionId",
                table: "Registers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Statuses_StatusId",
                table: "Registers",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
