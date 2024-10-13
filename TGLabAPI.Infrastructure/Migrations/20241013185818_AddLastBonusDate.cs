using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TGLabAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLastBonusDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastBonusDate",
                table: "Players",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastBonusDate",
                table: "Players");
        }
    }
}
