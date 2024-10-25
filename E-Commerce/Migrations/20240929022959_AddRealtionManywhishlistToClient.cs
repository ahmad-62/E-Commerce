using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class AddRealtionManywhishlistToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WhishLists_clientId",
                table: "WhishLists");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "WhishLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WhishLists_clientId",
                table: "WhishLists",
                column: "clientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WhishLists_clientId",
                table: "WhishLists");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "WhishLists");

            migrationBuilder.CreateIndex(
                name: "IX_WhishLists_clientId",
                table: "WhishLists",
                column: "clientId",
                unique: true);
        }
    }
}
