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
global using AutoMapper;

// ── Domain ───────────────────────────────────────────────────
global using InsuranceBrokerSystem.Domain.Entities;
global using InsuranceBrokerSystem.Domain.Entities.Master_Table;
global using InsuranceBrokerSystem.Domain.Enums.Master_Table;

// ── Application — Common Interfaces ─────────────────────────
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence;
global using InsuranceBrokerSystem.Application.Common.Interfaces.UnitOfWork;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Service.Financial;
global using InsuranceBrokerSystem.Application.Common.Interfaces.Persistence.Financial;
global using InsuranceBrokerSystem.Application.Interfaces.Master_Table;

// ── Application — DTOs ───────────────────────────────────────
global using InsuranceBrokerSystem.Application.DTOs.Financial.Account;
global using InsuranceBrokerSystem.Application.DTOs.Financial.InsuranceCompanyAccount;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line;

