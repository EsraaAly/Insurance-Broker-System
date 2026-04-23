using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceBrokerSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAndPositionEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Position",
                table: "ClientContacts",
                newName: "PositionName");

            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "ClientContacts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "ClientBankAccounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "banks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SwiftCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientContacts_PositionId",
                table: "ClientContacts",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBankAccounts_BankId",
                table: "ClientBankAccounts",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBankAccounts_banks_BankId",
                table: "ClientBankAccounts",
                column: "BankId",
                principalTable: "banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientContacts_positions_PositionId",
                table: "ClientContacts",
                column: "PositionId",
                principalTable: "positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientBankAccounts_banks_BankId",
                table: "ClientBankAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientContacts_positions_PositionId",
                table: "ClientContacts");

            migrationBuilder.DropTable(
                name: "banks");

            migrationBuilder.DropTable(
                name: "positions");

            migrationBuilder.DropIndex(
                name: "IX_ClientContacts_PositionId",
                table: "ClientContacts");

            migrationBuilder.DropIndex(
                name: "IX_ClientBankAccounts_BankId",
                table: "ClientBankAccounts");

            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "ClientContacts");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "ClientBankAccounts");

            migrationBuilder.RenameColumn(
                name: "PositionName",
                table: "ClientContacts",
                newName: "Position");
        }
    }
}
