using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceBrokerSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditClientTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                table: "InsuranceCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDate",
                table: "InsuranceCompanies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountPremium",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BlockedBy",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedDate",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsBlocked",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRejected",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RejectedBy",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedDate",
                table: "Clients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "InsuranceCompanies");

            migrationBuilder.DropColumn(
                name: "RejectedDate",
                table: "InsuranceCompanies");

            migrationBuilder.DropColumn(
                name: "AccountPremium",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BlockedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "BlockedDate",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsBlocked",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsRejected",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RejectedBy",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RejectedDate",
                table: "Clients");
        }
    }
}
