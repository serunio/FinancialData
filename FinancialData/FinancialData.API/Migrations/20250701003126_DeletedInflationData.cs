using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinancialData.API.Migrations
{
    /// <inheritdoc />
    public partial class DeletedInflationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Record_DataType_DataTypeId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Frequency_FrequencyId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_PresentationType_PresentationTypeId",
                table: "Record");

            migrationBuilder.DropTable(
                name: "InflationData");

            migrationBuilder.DropIndex(
                name: "IX_Record_DataTypeId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_FrequencyId",
                table: "Record");

            migrationBuilder.DropIndex(
                name: "IX_Record_PresentationTypeId",
                table: "Record");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InflationData",
                columns: table => new
                {
                    Key = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    InflationRate = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InflationData", x => x.Key);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Record_DataTypeId",
                table: "Record",
                column: "DataTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_FrequencyId",
                table: "Record",
                column: "FrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Record_PresentationTypeId",
                table: "Record",
                column: "PresentationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_DataType_DataTypeId",
                table: "Record",
                column: "DataTypeId",
                principalTable: "DataType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Frequency_FrequencyId",
                table: "Record",
                column: "FrequencyId",
                principalTable: "Frequency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_PresentationType_PresentationTypeId",
                table: "Record",
                column: "PresentationTypeId",
                principalTable: "PresentationType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
