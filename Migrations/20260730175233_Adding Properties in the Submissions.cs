using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Programming_Contest_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddingPropertiesintheSubmissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProblemCode",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "ProblemCode",
                table: "Submissions");
        }
    }
}
