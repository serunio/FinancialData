using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinancialData.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedRecordFrequencyDataTypePresentationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frequency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frequency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PresentationType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PresentationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DataTypeId = table.Column<int>(type: "integer", nullable: false),
                    FrequencyId = table.Column<int>(type: "integer", nullable: false),
                    PresentationTypeId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "DATE", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Record_DataType_DataTypeId",
                        column: x => x.DataTypeId,
                        principalTable: "DataType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Record_Frequency_FrequencyId",
                        column: x => x.FrequencyId,
                        principalTable: "Frequency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Record_PresentationType_PresentationTypeId",
                        column: x => x.PresentationTypeId,
                        principalTable: "PresentationType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record");

            migrationBuilder.DropTable(
                name: "DataType");

            migrationBuilder.DropTable(
                name: "Frequency");

            migrationBuilder.DropTable(
                name: "PresentationType");
        }
    }
}
