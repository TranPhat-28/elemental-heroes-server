using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elemental_heroes_server.Migrations
{
    /// <inheritdoc />
    public partial class UserSkillRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillUser",
                columns: table => new
                {
                    SkillsOwnedId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillUser", x => new { x.SkillsOwnedId, x.UserId });
                    table.ForeignKey(
                        name: "FK_SkillUser_Skills_SkillsOwnedId",
                        column: x => x.SkillsOwnedId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SkillUser_UserId",
                table: "SkillUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkillUser");
        }
    }
}
