using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastBook.Migrations
{
    /// <inheritdoc />
    public partial class AddTagsAndRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "Notes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Notes",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "backgroundColor",
                table: "Notes",
                newName: "BackgroundColor");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Notes",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoteTags",
                columns: table => new
                {
                    NotesId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TagsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteTags", x => new { x.NotesId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_NoteTags_Notes_NotesId",
                        column: x => x.NotesId,
                        principalTable: "Notes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoteTags_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteTags_TagsId",
                table: "NoteTags",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteTags");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Notes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Notes",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "BackgroundColor",
                table: "Notes",
                newName: "backgroundColor");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Notes",
                newName: "id");
        }
    }
}
