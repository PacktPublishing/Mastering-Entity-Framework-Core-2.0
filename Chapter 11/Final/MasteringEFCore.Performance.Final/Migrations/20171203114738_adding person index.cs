using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasteringEFCore.Performance.Final.Migrations
{
    public partial class addingpersonindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Person",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Address",
                maxLength: 6,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_FirstName_LastName",
                table: "Person",
                columns: new[] { "FirstName", "LastName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Person_FirstName_LastName",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Address");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 12,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Person",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
