# Insurance Broker System - Web Application

A comprehensive Angular web application for managing insurance brokerage operations, built with Angular 19, Bootstrap 5, and modern web technologies.

## Features

- **Authentication System**: Secure login/logout functionality with JWT-like token management
- **Dashboard**: Overview with statistics, charts, and recent activities
- **Client Management**: Complete CRUD operations for client records
- **Financial Management**: Account management and financial operations
- **Master Tables**: Comprehensive management of insurance-related data:
  - Business Activities
  - Insurance Classes & Lines of Business
  - Insurance Companies & Products
  - Locations & Nationalities
  - Policy Types & Source of Income
- **Responsive Design**: Mobile-friendly interface using Bootstrap 5
- **Modern UI**: Clean, professional design with Font Awesome icons

## Tech Stack

- **Frontend**: Angular 19 (Standalone Components)
- **Styling**: Bootstrap 5 + SCSS
- **Icons**: Font Awesome
- **HTTP Client**: Angular HttpClient
- **Routing**: Angular Router with lazy loading
- **Authentication**: Custom auth service with route guards

## Prerequisites

- Node.js (v18 or higher)
- Angular CLI (`npm install -g @angular/cli`)
- .NET 8+ Backend API (running on https://localhost:7001)

## Getting Started

### Installation

1. Clone the repository
2. Navigate to the web project directory:
   ```bash
   cd InsuranceBrokerSystem.Web
   ```
3. Install dependencies:
   ```bash
   npm install
   ```

### Development Server

Start the development server:

```bash
ng serve
```

The application will be available at `http://localhost:4200/`

### Build for Production

```bash
ng build
```

The build artifacts will be stored in the `dist/` directory.

## Configuration

### API Configuration

Update the API base URL in `src/app/services/api.service.ts`:

```typescript
private baseUrl = 'https://localhost:7001/api/v1'; // Update with your API base URL
```

### Authentication

Demo credentials for testing:
- Username: `admin`
- Password: `password`

## Project Structure

```
src/app/
|-- components/
|   |-- auth/           # Authentication components
|   |-- dashboard/      # Dashboard component
|   |-- layout/         # Layout components (header, main-layout)
|-- services/
|   |-- api.service.ts  # API communication service
|   |-- auth.service.ts # Authentication service
|-- guards/
|   |-- auth.guard.ts   # Authentication route guard
|-- modules/
|   |-- clients/        # Client management module
|   |-- financial/      # Financial management module
|   |-- master-table/   # Master tables module
|-- app.routes.ts       # Application routing
|-- app.config.ts       # Application configuration
```

## Available Scripts

- `ng serve` - Start development server
- `ng build` - Build for production
- `ng test` - Run unit tests
- `ng e2e` - Run end-to-end tests
- `ng lint` - Run linting

## API Integration

The application is designed to work with the .NET Web API backend. The following API endpoints are supported:

- **Authentication**: Login/logout functionality
- **Clients**: Full CRUD operations
- **Financial**: Account management and operations
- **Master Tables**: Complete management of reference data

## Development Notes

- Uses Angular standalone components (no NgModule required for most components)
- Lazy loading for feature modules
- Responsive design with Bootstrap 5
- Custom SCSS styling with CSS variables
- Route-based authentication with guards

## Contributing

1. Follow the existing code style and patterns
2. Use TypeScript for all new code
3. Add appropriate tests for new features
4. Update documentation as needed

## License

This project is part of the Insurance Broker System suite.
