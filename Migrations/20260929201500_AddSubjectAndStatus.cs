using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentStorer.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Module",
                table: "Assignments",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Assignments",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Assignments",
                newName: "Module");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Assignments",
                newName: "FilePath");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
