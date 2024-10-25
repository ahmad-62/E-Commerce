using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce.Migrations
{
    /// <inheritdoc />
    public partial class RenameWishList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "WhishLists", 
                newName: "WishLists" 
            );
            migrationBuilder.RenameTable(
    name: "WhishListitems",  // Current table name
    newName: "WishListItems" // New table name
);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
    name: "WishLists",   // New table name
    newName: "WhishLists" // Old table name
); migrationBuilder.RenameTable(
    name: "WishListItems",   // New table name
    newName: "WhishListitems" // Old table name
);
        }
    }
}
