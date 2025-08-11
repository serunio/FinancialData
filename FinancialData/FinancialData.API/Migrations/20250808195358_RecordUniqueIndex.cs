using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialData.API.Migrations
{
    /// <inheritdoc />
    public partial class RecordUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Record_DataTypeId_FrequencyId_PresentationTypeId_Date",
                table: "Record",
                columns: new[] { "DataTypeId", "FrequencyId", "PresentationTypeId", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Record_DataTypeId_FrequencyId_PresentationTypeId_Date",
                table: "Record");
        }
    }
}
