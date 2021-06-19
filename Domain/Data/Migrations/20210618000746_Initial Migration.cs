using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Domain.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicCredentials",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: true),
                    Firstname = table.Column<string>(maxLength: 500, nullable: false),
                    Lastname = table.Column<string>(maxLength: 500, nullable: false),
                    Email = table.Column<string>(maxLength: 500, nullable: false),
                    StartYear = table.Column<DateTime>(nullable: false),
                    EndYear = table.Column<DateTime>(nullable: false),
                    TranscriptUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Course = table.Column<string>(nullable: true),
                    GPA = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    AuditLogId = table.Column<Guid>(nullable: false),
                    EventDateUtc = table.Column<DateTimeOffset>(nullable: false),
                    EventType = table.Column<string>(nullable: true),
                    TableName = table.Column<string>(nullable: true),
                    RecordId = table.Column<string>(nullable: true),
                    ColumnName = table.Column<string>(nullable: true),
                    OriginalValue = table.Column<string>(nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    Ip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.AuditLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicCredentials");

            migrationBuilder.DropTable(
                name: "AuditLogs");
        }
    }
}
