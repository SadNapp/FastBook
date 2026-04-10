using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBook.Migrations
{
    /// <inheritdoc />
    public partial class AdvancedFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Notes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Notes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Notes");
        }
    }
}
