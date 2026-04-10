// =====================================
// Global Using Directives — InsuranceBrokerSystem.Application
// =====================================
global using InsuranceBrokerSystem.Application.Common.Models;

// ── BCL ──────────────────────────────────────────────────────
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Linq.Expressions;
global using System.Text;
global using System.Threading.Tasks;

// ── Third-party ──────────────────────────────────────────────
global using Mapster;
global using MediatR;
global using FluentValidation;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
// ── Domain ───────────────────────────────────────────────────
global using InsuranceBrokerSystem.Domain.Entities.Financial;
global using InsuranceBrokerSystem.Domain.Entities.MasterTable;
global using InsuranceBrokerSystem.Domain.Enums.Master_Table;
global using InsuranceBrokerSystem.Domain.Entities.Clients;

// ── Application — Common Interfaces ─────────────────────────
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence;
global using InsuranceBrokerSystem.Application.Common.Interfaces.UnitOfWork;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Clients;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Financial;
global using InsuranceBrokerSystem.Application.Interfaces.Master_Table;
global using InsuranceBrokerSystem.Application.Common.Mapping;
// ── Application — DTOs ───────────────────────────────────────
global using InsuranceBrokerSystem.Application.DTOs.Financial.Account;
global using InsuranceBrokerSystem.Application.DTOs.Financial.InsuranceCompanyAccount;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line;
global using InsuranceBrokerSystem.Application.DTOs.Client;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.Nationality;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.SourceOfIncome;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.PolicyType;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.BusinessActivity;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.Location;



