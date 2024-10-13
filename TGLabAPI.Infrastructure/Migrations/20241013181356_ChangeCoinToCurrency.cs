using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TGLabAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoinToCurrency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coin",
                table: "Wallets",
                newName: "Currency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Wallets",
                newName: "Coin");
        }
    }
}
