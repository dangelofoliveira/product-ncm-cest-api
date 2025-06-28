using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CestNcm.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMvaSubstituto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "mva_substituto",
                table: "produtos_cest",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mva_substituto",
                table: "produtos_cest");
        }
    }
}
