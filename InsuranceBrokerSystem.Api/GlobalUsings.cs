// =====================================
// Global Using Directives — InsuranceBrokerSystem.Api
// =====================================
global using InsuranceBrokerSystem.Application.Common.Models;
global using InsuranceBrokerSystem.Api.Extensions;

// ── BCL ──────────────────────────────────────────────────────
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Reflection;
global using System.Text;
global using System.Threading.Tasks;
global using MediatR;
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using Mapster;


// ── ASP.NET Core ─────────────────────────────────────────────
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

// ── Third-party ──────────────────────────────────────────────

// ── UI (API Routes) ──────────────────────────────────────────
global using InsuranceBrokerSystem.UI;

// ── Application — Interfaces ─────────────────────────────────
global using InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial;
global using InsuranceBrokerSystem.Domain.Enums.Master_Table;
global using InsuranceBrokerSystem.Domain.Entities.MasterTable;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Financial;
global using InsuranceBrokerSystem.Application.Common.Interfaces.UnitOfWork;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Clients;
// ── Application — DTOs ───────────────────────────────────────
global using InsuranceBrokerSystem.Application.DTOs.Financial.Account;
global using InsuranceBrokerSystem.Application.DTOs.Financial.InsuranceCompanyAccount;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table;

// ── Application — Services ───────────────────────────────────
global using InsuranceBrokerSystem.Application.Services.Master_Table;
global using InsuranceBrokerSystem.Application.Services.Financial;
global using InsuranceBrokerSystem.Application.Interfaces.Master_Table;
global using InsuranceBrokerSystem.Application.Common.Mapping;
// ── Application — Features ───────────────────────────────────
global using InsuranceBrokerSystem.Application.Features.Financial.Accounts.Queries;
global using InsuranceBrokerSystem.Application.Features.InsuranceClasses.Commands.AddInsuranceClass;
global using InsuranceBrokerSystem.Application.Features.InsuranceClasses.Commands.DeleteInsuranceClass;
global using InsuranceBrokerSystem.Application.Features.InsuranceClasses.Commands.UpdateInsuranceClass;
global using InsuranceBrokerSystem.Application.Features.InsuranceClasses.Queries.GetAllInsuranceClasses;
global using InsuranceBrokerSystem.Application.Features.InsuranceClasses.Queries.GetInsuranceClassById;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.AddInsuranceCompany;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.UpdateInsuranceCompany;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.DeleteInsuranceCompany;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.ApproveInsuranceCompany;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.RejectInsuranceCompany;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetAllInsuranceCompanies;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceCompanyByName;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceCompanyById;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceContactsByCompanyId;
global using InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Commands.AddInsuranceLOB;
global using InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Commands.UpdateInsuranceLOB;
global using InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Commands.DeleteInsuranceLOB;
global using InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Queries.GetAllInsuranceLOBs;
global using InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Queries.GetInsuranceLOBByClassId;
global using InsuranceBrokerSystem.Application.Features.InsuranceLOBs.Queries.GetInsuranceLOBById;
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Queries.GetInsuranceProductsByCompanyId;
global using InsuranceBrokerSystem.Application.Features.BusinessActivities.Queries.GetAllBusinessActivities;
global using InsuranceBrokerSystem.Application.Features.BusinessActivities.Queries.GetBusinessActivityById;
global using InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.AddBusinessActivity;
global using InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.DeleteBusinessActivity;
global using InsuranceBrokerSystem.Application.Features.BusinessActivities.Commands.UpdateBusinessActivity;
global using InsuranceBrokerSystem.Application.Features.Locations.Queries.GetAllLocations;
global using InsuranceBrokerSystem.Application.Features.Locations.Queries.GetLocationById;
global using InsuranceBrokerSystem.Application.Features.Locations.Commands.AddLocation;
global using InsuranceBrokerSystem.Application.Features.Locations.Commands.UpdateLocation;
global using InsuranceBrokerSystem.Application.Features.Locations.Commands.DeleteLocation;
global using InsuranceBrokerSystem.Application.Features.Nationalities.Commands.AddNationality;
global using InsuranceBrokerSystem.Application.Features.Nationalities.Commands.DeleteNationality;
global using InsuranceBrokerSystem.Application.Features.Nationalities.Commands.UpdateNationality;
global using InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.AddPolicyType;
global using InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.UpdatePolicyType;
global using InsuranceBrokerSystem.Application.Features.PolicyTypes.Commands.DeletePolicyType;
global using InsuranceBrokerSystem.Application.Features.PolicyTypes.Queries.GetAllPolicyTypes;
global using InsuranceBrokerSystem.Application.Features.PolicyTypes.Queries.GetPolicyTypeById;
global using InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.AddSourceOfIncome;
global using InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.DeleteSourceOfIncome;
global using InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Commands.UpdateSourceOfIncome;
global using InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Queries.GetAllSourceOfIncomes;
global using InsuranceBrokerSystem.Application.Features.SourceOfIncomes.Queries.GetSourceOfIncomeById;
global using InsuranceBrokerSystem.Application.Features.Nationalities.Queries.GetAllNationalities;
global using InsuranceBrokerSystem.Application.Features.Nationalities.Queries.GetNationalityById;

// ── Infrastructure ───────────────────────────────────────────
global using InsuranceBrokerSystem.Infrastructure.Data;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Financial;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Clients;
// ── Mapping Profiles ─────────────────────────────────────────



global using InsuranceBrokerSystem.Domain.Entities.Financial;

