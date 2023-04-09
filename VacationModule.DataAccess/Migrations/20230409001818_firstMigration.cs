using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VacationModule.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<DateTime>>(
                name: "requestedDays",
                table: "VacationRequests",
                type: "timestamp without time zone[]",
                nullable: false,
                oldClrType: typeof(List<DateTime>),
                oldType: "timestamp with time zone[]",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<DateTime>>(
                name: "requestedDays",
                table: "VacationRequests",
                type: "timestamp with time zone[]",
                nullable: true,
                oldClrType: typeof(List<DateTime>),
                oldType: "timestamp without time zone[]");
        }
    }
}
