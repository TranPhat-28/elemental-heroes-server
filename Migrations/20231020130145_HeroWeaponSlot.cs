using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elemental_heroes_server.Migrations
{
    /// <inheritdoc />
    public partial class HeroWeaponSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WeaponId",
                table: "Heroes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_WeaponId",
                table: "Heroes",
                column: "WeaponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Weapons_WeaponId",
                table: "Heroes",
                column: "WeaponId",
                principalTable: "Weapons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Weapons_WeaponId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_WeaponId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "WeaponId",
                table: "Heroes");
        }
    }
}
