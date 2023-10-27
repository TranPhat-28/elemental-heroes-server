using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elemental_heroes_server.Migrations
{
    /// <inheritdoc />
    public partial class HeroSkillsSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SkillAId",
                table: "Heroes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillBId",
                table: "Heroes",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SkillCId",
                table: "Heroes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_SkillAId",
                table: "Heroes",
                column: "SkillAId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_SkillBId",
                table: "Heroes",
                column: "SkillBId");

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_SkillCId",
                table: "Heroes",
                column: "SkillCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Skills_SkillAId",
                table: "Heroes",
                column: "SkillAId",
                principalTable: "Skills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Skills_SkillBId",
                table: "Heroes",
                column: "SkillBId",
                principalTable: "Skills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Skills_SkillCId",
                table: "Heroes",
                column: "SkillCId",
                principalTable: "Skills",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Skills_SkillAId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Skills_SkillBId",
                table: "Heroes");

            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Skills_SkillCId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_SkillAId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_SkillBId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_SkillCId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "SkillAId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "SkillBId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "SkillCId",
                table: "Heroes");
        }
    }
}
