using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsuranceBrokerSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessActivity",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SourceOfIncome",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "PolicyTypeName",
                table: "policyTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "NationalityName",
                table: "nationalities",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RelationshipStatus",
                table: "Clients",
                newName: "ClientRelationshipStatus");

            migrationBuilder.RenameColumn(
                name: "BusinessActivityName",
                table: "businessActivities",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "policyTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "nationalities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "nationalities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "RegistrationStatus",
                table: "Clients",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BusinessActivityId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SourceOfIncomeId",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "businessActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sourceOfIncomes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sourceOfIncomes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_BusinessActivityId",
                table: "Clients",
                column: "BusinessActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LocationId",
                table: "Clients",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_NationalityId",
                table: "Clients",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PolicyTypeId",
                table: "Clients",
                column: "PolicyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_SourceOfIncomeId",
                table: "Clients",
                column: "SourceOfIncomeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_businessActivities_BusinessActivityId",
                table: "Clients",
                column: "BusinessActivityId",
                principalTable: "businessActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_locations_LocationId",
                table: "Clients",
                column: "LocationId",
                principalTable: "locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_nationalities_NationalityId",
                table: "Clients",
                column: "NationalityId",
                principalTable: "nationalities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_policyTypes_PolicyTypeId",
                table: "Clients",
                column: "PolicyTypeId",
                principalTable: "policyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_sourceOfIncomes_SourceOfIncomeId",
                table: "Clients",
                column: "SourceOfIncomeId",
                principalTable: "sourceOfIncomes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clients_businessActivities_BusinessActivityId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_locations_LocationId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_nationalities_NationalityId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_policyTypes_PolicyTypeId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_sourceOfIncomes_SourceOfIncomeId",
                table: "Clients");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "sourceOfIncomes");

            migrationBuilder.DropIndex(
                name: "IX_Clients_BusinessActivityId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_LocationId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_NationalityId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_PolicyTypeId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_SourceOfIncomeId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "policyTypes");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "nationalities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "nationalities");

            migrationBuilder.DropColumn(
                name: "BusinessActivityId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "SourceOfIncomeId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "businessActivities");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "policyTypes",
                newName: "PolicyTypeName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "nationalities",
                newName: "NationalityName");

            migrationBuilder.RenameColumn(
                name: "ClientRelationshipStatus",
                table: "Clients",
                newName: "RelationshipStatus");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "businessActivities",
                newName: "BusinessActivityName");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationStatus",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BusinessActivity",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "Clients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SourceOfIncome",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
