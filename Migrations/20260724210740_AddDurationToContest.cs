using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Programming_Contest_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddDurationToContest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DurationMinutes",
                table: "Contests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationMinutes",
                table: "Contests");
        }
    }
}
