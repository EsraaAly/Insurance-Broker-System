// ============================================================
// Global Using Directives — InsuranceBrokerSystem.UI
// All namespaces listed here are automatically available in
// every .cs file within this project (C# 10 / .NET 6+).
// ============================================================

// ── BCL & WPF ────────────────────────────────────────────────
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using System.Net.Http;
global using System.Net.Http.Json;
global using System.Runtime.CompilerServices;
global using System.ComponentModel;
global using System.Windows;
global using System.Windows.Controls;
global using System.Windows.Data;
global using System.Windows.Documents;
global using System.Windows.Input;
global using System.Windows.Media;
global using System.Windows.Media.Imaging;
global using System.Windows.Shapes;
global using System.Collections.ObjectModel;
global using Azure;
global using System.Diagnostics;
global using System.IO;

global using Microsoft.Win32;


// ── Third-party ──────────────────────────────────────────────
global using Mapster;

// ── Project Namespaces ───────────────────────────────────────
global using InsuranceBrokerSystem.UI.Views;
global using InsuranceBrokerSystem.UI.Services;
global using InsuranceBrokerSystem.UI.Views.MasterTable;
global using InsuranceBrokerSystem.UI.Views.Financial;
global using InsuranceBrokerSystem.UI.Services.Master_Table;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.InsuranceCompany;
global using InsuranceBrokerSystem.Application.DTOs.Master_Table.Insurance_Class_and_Line;
global using InsuranceBrokerSystem.Application.DTOs.Financial.Account;
global using InsuranceBrokerSystem.Application.DTOs.Financial.InsuranceCompanyAccount;
global using InsuranceBrokerSystem.UI.Services.Financial;
global using InsuranceBrokerSystem.Domain.Enums.Master_Table;
global using InsuranceBrokerSystem.Application.DTOs.Client;
global using InsuranceBrokerSystem.Domain.Enums.Client;
global using InsuranceBrokerSystem.UI.Services.Clients;
global using InsuranceBrokerSystem.UI.Views.MasterData;
global using InsuranceBrokerSystem.Domain.Entities.MasterTable;


