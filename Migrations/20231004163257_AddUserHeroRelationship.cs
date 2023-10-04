using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace elemental_heroes_server.Migrations
{
    /// <inheritdoc />
    public partial class AddUserHeroRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Heroes",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Heroes_UserId",
                table: "Heroes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Heroes_Users_UserId",
                table: "Heroes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Heroes_Users_UserId",
                table: "Heroes");

            migrationBuilder.DropIndex(
                name: "IX_Heroes_UserId",
                table: "Heroes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Heroes");
        }
    }
}
