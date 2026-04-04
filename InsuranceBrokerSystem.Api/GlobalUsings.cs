// ============================================================
// Global Using Directives — InsuranceBrokerSystem.Api
// All namespaces listed here are automatically available in
// every .cs file within this project (C# 10 / .NET 6+).
// ============================================================

// ── BCL ──────────────────────────────────────────────────────
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Reflection;
global using System.Text;
global using System.Threading.Tasks;

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


// ── Infrastructure ───────────────────────────────────────────
global using InsuranceBrokerSystem.Infrastructure.Data;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Financial;
global using InsuranceBrokerSystem.Infrastructure.UnitOfWork;

// ── Mapping Profiles ─────────────────────────────────────────
global using InsuranceBrokerSystem.Application.Common.Mapping.Financial;
global using InsuranceBrokerSystem.Application.Common.Mapping.MasterTable;
global using InsuranceBrokerSystem.UI.UIMappings;
global using InsuranceBrokerSystem.UI.UI.MasterTable;

