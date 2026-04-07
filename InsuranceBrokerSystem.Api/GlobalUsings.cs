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
global using FluentValidation;
global using FluentValidation.AspNetCore;
global using MediatR;


// ── ASP.NET Core ─────────────────────────────────────────────
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;

// ── Third-party ──────────────────────────────────────────────
global using AutoMapper;

// ── UI (API Routes) ──────────────────────────────────────────
global using InsuranceBrokerSystem.UI;

// ── Application — Interfaces ─────────────────────────────────
global using InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial;
global using InsuranceBrokerSystem.Domain.Enums.Master_Table;
global using InsuranceBrokerSystem.Domain.Entities.Master_Table;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Financial;
global using InsuranceBrokerSystem.Application.Common.Interfaces.UnitOfWork;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Clients;
global using InsuranceBrokerSystem.Application.Common.Mapping.Clients;
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
global using InsuranceBrokerSystem.Application.Features.InsuranceCompanies.Commands.GenerateInsuranceCompanyAccounts;

// ── Infrastructure ───────────────────────────────────────────
global using InsuranceBrokerSystem.Infrastructure.Data;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Financial;
global using InsuranceBrokerSystem.Infrastructure.UnitOfWork;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Clients;
// ── Mapping Profiles ─────────────────────────────────────────
global using InsuranceBrokerSystem.Application.Common.Mapping.Financial;
global using InsuranceBrokerSystem.Application.Common.Mapping.MasterTable;
global using InsuranceBrokerSystem.UI.UIMappings;
global using InsuranceBrokerSystem.UI.UI.MasterTable;

