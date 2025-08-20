# OKRs .NET 9.0 Web Application

Objectives and Key Results (OKRs) tooling built with .NET 9.0, ASP.NET Core, Entity Framework Core, and Google OAuth authentication. This is an ASP.NET MVC web application with SQL Server database support.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Working Effectively

### Bootstrap, Build, and Test the Repository:

**CRITICAL: Install .NET 9.0 SDK first** (not available by default on most systems):
```bash
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 9.0
export PATH="$HOME/.dotnet:$PATH"
```

**Essential setup commands:**
```bash
# Install Entity Framework tools globally
dotnet tool install --global dotnet-ef

# Restore packages (takes ~25 seconds. NEVER CANCEL. Set timeout to 60+ minutes)
dotnet restore

# Build the solution (takes ~10-15 seconds. NEVER CANCEL. Set timeout to 30+ minutes)
dotnet build
```

**Expected build output:** Build succeeds with 8 warnings but is fully functional. Warnings are related to obsolete APIs and MVC partial rendering - these are non-critical.

### Database Setup (Required for Full Functionality):

The application requires SQL Server with two database contexts:

**Option 1: With SQL Server (Full functionality):**
```bash
# Update connection string in appsettings.Development.json:
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=okrs;Integrated Security=True;TrustServerCertificate=True;"
  }
}

# Apply database migrations for both contexts
dotnet ef database update --context ApplicationDbContext
dotnet ef database update --context ObjectivesDbContext
```

**Option 2: Development without database (Limited functionality):**
Create `/OKRs/appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=okrs;Integrated Security=True;TrustServerCertificate=True;"
  },
  "Authentication": {
    "Google": {
      "ClientId": "test-client-id",
      "ClientSecret": "test-client-secret",
      "DomainFilter": ""
    }
  }
}
```

### Run the Application:

**Development server (takes ~5 seconds to start. NEVER CANCEL. Set timeout to 30+ minutes):**
```bash
cd OKRs
export PATH="$HOME/.dotnet:$PATH"
ASPNETCORE_ENVIRONMENT=Development dotnet run --urls=http://localhost:5000
```

**Expected behavior:** 
- App starts on http://localhost:5000
- Redirects to /Account/Login 
- Shows Google OAuth login button
- Navigation includes: Home, My Objectives, About, Import Data
- Displays properly with Bootstrap styling

### Code Quality and Formatting:

**Format code (takes ~5 seconds):**
```bash
dotnet format
```

**Validate formatting without changes:**
```bash
dotnet format --verify-no-changes
```

## Validation

**Always test the complete flow after making changes:**

1. **Build validation:** `dotnet build` should succeed with 8 expected warnings
2. **Run validation:** Application should start and respond on http://localhost:5000
3. **UI validation:** Login page should display with Google OAuth button and proper navigation
4. **Database validation (if configured):** EF migrations should apply without errors

**Manual validation steps:**
- Navigate to http://localhost:5000
- Verify redirect to /Account/Login
- Check that page displays "Log in" heading
- Confirm Google authentication button is present
- Verify navigation menu shows: Home, My Objectives, About, Import Data
- Confirm footer shows build version

## Configuration Requirements

**Minimal development configuration (in appsettings.Development.json):**
- `ConnectionStrings:DefaultConnection` - SQL Server connection string
- `Authentication:Google:ClientId` - Google OAuth client ID (can use placeholder for dev)
- `Authentication:Google:ClientSecret` - Google OAuth secret (can use placeholder for dev)
- `Authentication:Google:DomainFilter` - Optional email domain restriction

**Production configuration additionally requires:**
- Valid Google OAuth credentials from https://console.developers.google.com
- SQL Server database with applied migrations
- HTTPS certificate configuration

## Project Structure

**Key directories:**
- `OKRs/Controllers/` - MVC controllers (Account, Home, Import, KeyResult, Manage, Objective, User)
- `OKRs/Models/` - Data models (ApplicationUser, Objective, KeyResult, ViewModels)
- `OKRs/Views/` - Razor views organized by controller
- `OKRs/Data/` - Entity Framework contexts and migrations
- `OKRs/wwwroot/` - Static assets (CSS, JS, images)

**Important files:**
- `OKRs/Program.cs` - Application configuration and startup
- `OKRs/appsettings.json` - Base configuration
- `OKRs/appsettings.Development.json` - Development overrides
- `OKRs.sln` - Solution file
- `.github/workflows/` - CI/CD pipelines (build.yml, release.yml)

## Database Contexts

**ApplicationDbContext:** ASP.NET Identity tables for user management
- Migration: `00000000000000_CreateIdentitySchema`
- Migration: `20180212214157_AddNameColumn` 
- Migration: `20180906081834_UserAddInactiveColumn`

**ObjectivesDbContext:** Business domain tables
- Migration: `20180212203958_ObjectivesInitialCreate`

**Useful EF commands:**
```bash
# List contexts
dotnet ef dbcontext list

# List migrations for specific context
dotnet ef migrations list --context ApplicationDbContext
dotnet ef migrations list --context ObjectivesDbContext

# Update database
dotnet ef database update --context [ContextName]
```

## Common Issues and Solutions

**Issue:** "The current .NET SDK does not support targeting .NET 9.0"
**Solution:** Install .NET 9.0 SDK using the installation script above

**Issue:** "Value cannot be null. (Parameter 'connectionString')"
**Solution:** Create appsettings.Development.json with connection string

**Issue:** Build warnings about obsolete APIs
**Solution:** These are expected and non-critical. 8 warnings is normal for this codebase

**Issue:** Database connection errors during EF commands
**Solution:** This is expected without SQL Server. Commands will show migrations but note database is inaccessible

## Performance Expectations

**Timing expectations (NEVER CANCEL these operations):**
- Package restore: 25-30 seconds
- Build: 10-15 seconds  
- Application startup: 5-10 seconds
- EF migrations: 5-15 seconds each
- Code formatting: 5 seconds

**Always set appropriate timeouts:**
- Build commands: 30+ minutes timeout
- Package operations: 60+ minutes timeout
- Application startup: 30+ minutes timeout

## GitHub Actions

**CI/CD pipelines:**
- `.github/workflows/build.yml` - Builds on .NET 9.0 for PRs and main branch
- `.github/workflows/release.yml` - Deploys to Azure Web App on main branch

Both workflows expect .NET 9.0 and will install it automatically in the CI environment.