using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MasteringEFCore.MultiTenancy.Final.Migrations
{
    public partial class TenantandPersonrelationshipwasstreamlined : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tenants_Person_PersonId",
                table: "Tenants");

            migrationBuilder.DropIndex(
                name: "IX_Tenants_PersonId",
                table: "Tenants");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Tenants",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "Person",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Person_TenantId",
                table: "Person",
                column: "TenantId",
                unique: true,
                filter: "[TenantId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Tenants_TenantId",
                table: "Person",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Tenants_TenantId",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_TenantId",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Tenants",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PersonId",
                table: "Tenants",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tenants_Person_PersonId",
                table: "Tenants",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
