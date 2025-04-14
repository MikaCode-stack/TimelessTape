using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimelessTapes.Migrations
{
    /// <inheritdoc />
    public partial class fixedDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "Users",
                newName: "accessType1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "accessType1",
                table: "Users",
                newName: "Discriminator");
        }
    }
}
