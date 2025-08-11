using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialData.API.Migrations
{
    /// <inheritdoc />
    public partial class changed_DATE_to_date : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Record",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "DATE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Record",
                type: "DATE",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
