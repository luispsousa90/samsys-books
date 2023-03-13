using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BooksApi.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Deleted",
                table: "Books",
                newName: "IsDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Books",
                newName: "Deleted");
        }
    }
}
