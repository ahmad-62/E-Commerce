using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    public partial class renamewishlistindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the old foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_Products_ProductId",
                table: "WishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhishListItems_WhishLists_whishlistId",
                table: "WishListItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WhishLists_Clients_clientId",
                table: "WishLists");

            

            // Rename the foreign key indexes to match the new names
            migrationBuilder.RenameIndex(
                name: "IX_WhishListItems_whishlistId",
                table: "WishlistItems",
                newName: "IX_WishlistItems_WishlistId");

            migrationBuilder.RenameIndex(
                name: "IX_WhishLists_clientId",
                table: "WishLists",
                newName: "IX_WishLists_clientId");

           

            // Add foreign keys with the correct table and column names
            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_Products_ProductId",
                table: "WishListItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishlistItems_WishLists_WishlistId",
                table: "WishListItems",
                column: "whishlistId",
                principalTable: "WishLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WishLists_Clients_clientId",
                table: "WishLists",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert foreign keys and table names in case of rollback

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_Products_ProductId",
                table: "WishlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishlistItems_WishLists_WishlistId",
                table: "WishlistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WishLists_Clients_clientId",
                table: "WishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishLists",
                table: "WishLists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WishlistItems",
                table: "WishlistItems");

            migrationBuilder.RenameTable(
                name: "WishLists",
                newName: "WhishLists");

            migrationBuilder.RenameTable(
                name: "WishlistItems",
                newName: "WhishListItems");

            migrationBuilder.RenameIndex(
                name: "IX_WishLists_clientId",
                table: "WhishLists",
                newName: "IX_WhishLists_clientId");

            migrationBuilder.RenameIndex(
                name: "IX_WishlistItems_WishlistId",
                table: "WhishListItems",
                newName: "IX_WhishListItems_whishlistId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhishLists",
                table: "WhishLists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WhishListItems",
                table: "WhishListItems",
                columns: new[] { "ProductId", "whishlistId" });

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_Products_ProductId",
                table: "WhishListItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhishListItems_WhishLists_whishlistId",
                table: "WhishListItems",
                column: "whishlistId",
                principalTable: "WhishLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WhishLists_Clients_clientId",
                table: "WhishLists",
                column: "clientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
