using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicantTracking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigrationApplicant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "candidates",
                schema: "dbo",
                columns: table => new
                {
                    IdCandidate = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(80)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(150)", nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidates", x => x.IdCandidate);
                });

            migrationBuilder.CreateTable(
                name: "timelines",
                schema: "dbo",
                columns: table => new
                {
                    IdTimeline = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAggregateRoot = table.Column<int>(type: "int", nullable: false),
                    IdTimelineType = table.Column<byte>(type: "tinyint", nullable: false),
                    OldData = table.Column<string>(type: "varchar(max)", nullable: true),
                    NewData = table.Column<string>(type: "varchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_timelines", x => x.IdTimeline);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "candidates",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "timelines",
                schema: "dbo");
        }
    }
}
