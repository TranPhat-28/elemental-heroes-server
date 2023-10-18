using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elemental_heroes_server.Migrations
{
    /// <inheritdoc />
    public partial class AddUserWeaponRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserWeapon",
                columns: table => new
                {
                    UsersId = table.Column<int>(type: "integer", nullable: false),
                    WeaponsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserWeapon", x => new { x.UsersId, x.WeaponsId });
                    table.ForeignKey(
                        name: "FK_UserWeapon_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserWeapon_Weapons_WeaponsId",
                        column: x => x.WeaponsId,
                        principalTable: "Weapons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserWeapon_WeaponsId",
                table: "UserWeapon",
                column: "WeaponsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserWeapon");
        }
    }
}
