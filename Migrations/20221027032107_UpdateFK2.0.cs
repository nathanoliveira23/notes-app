using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Notes.Migrations
{
    public partial class UpdateFK20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Notes_NoteId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Notes_NoteId",
                table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Tags",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Links",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Notes_NoteId",
                table: "Links",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Notes_NoteId",
                table: "Tags",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Notes_NoteId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Notes_NoteId",
                table: "Tags");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Tags",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<int>(
                name: "NoteId",
                table: "Links",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Notes_NoteId",
                table: "Links",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Notes_NoteId",
                table: "Tags",
                column: "NoteId",
                principalTable: "Notes",
                principalColumn: "Id");
        }
    }
}
