using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RunGroupWebApp1.Migrations
{
    public partial class creationEditDatesInRace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "Races",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "creationDate",
                table: "Races",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "creationDate",
                table: "Races");
        }
    }
}
