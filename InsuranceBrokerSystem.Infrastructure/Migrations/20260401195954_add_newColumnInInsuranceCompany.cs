using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceBrokerSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_newColumnInInsuranceCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPosting",
                table: "accounts",
                newName: "IsPostable");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedBy",
                table: "InsuranceCompanies",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                table: "InsuranceCompanies",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "InsuranceCompanies");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                table: "InsuranceCompanies");

            migrationBuilder.RenameColumn(
                name: "IsPostable",
                table: "accounts",
                newName: "IsPosting");
        }
    }
}
