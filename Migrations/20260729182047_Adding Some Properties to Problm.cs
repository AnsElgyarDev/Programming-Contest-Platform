using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Programming_Contest_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddingSomePropertiestoProblm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // migrationBuilder.DropColumn(
            //     name: "DurationMinutes",
            //     table: "Contests");

            migrationBuilder.AddColumn<string>(
                name: "ProblemDescription",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProblemDescription",
                table: "Problems");

            // migrationBuilder.AddColumn<int>(
            //     name: "DurationMinutes",
            //     table: "Contests",
            //     type: "int",
            //     nullable: false,
            //     defaultValue: 0);
        }
    }
}
