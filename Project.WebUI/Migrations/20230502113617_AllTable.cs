using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.WebUI.Migrations
{
    public partial class AllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "Registers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Registers_ProfessionId",
                table: "Registers",
                column: "ProfessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Registers_Professions_ProfessionId",
                table: "Registers",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registers_Professions_ProfessionId",
                table: "Registers");

            migrationBuilder.DropIndex(
                name: "IX_Registers_ProfessionId",
                table: "Registers");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "Registers");
        }
    }
}
