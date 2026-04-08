// ============================================================
// Global Using Directives — InsuranceBrokerSystem.Infrastructure
// All namespaces listed here are automatically available in
// every .cs file within this project (C# 10 / .NET 6+).
// ============================================================

// ── BCL ──────────────────────────────────────────────────────
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Text;
global using System.Threading.Tasks;

// ── EF Core ──────────────────────────────────────────────────
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
// ── Domain ───────────────────────────────────────────────────
global using InsuranceBrokerSystem.Domain.Entities;
global using InsuranceBrokerSystem.Domain.Entities.MasterTable;
global using InsuranceBrokerSystem.Domain.Entities.Clients;
global using InsuranceBrokerSystem.Domain.Enums.Client;
global using InsuranceBrokerSystem.Domain.Entities.Financial;
// ── Application — Interfaces ─────────────────────────────────
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence;
global using InsuranceBrokerSystem.Application.Common.Interfaces.UnitOfWork;
global using InsuranceBrokerSystem.Application.Interfaces.Master_Table;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Financial;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Clients;

// ── Infrastructure — Data ────────────────────────────────────
global using InsuranceBrokerSystem.Infrastructure.Data;
global using InsuranceBrokerSystem.Infrastructure.Repositories;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Master_Table;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Financial;
global using InsuranceBrokerSystem.Infrastructure.Repositories.Clients;
