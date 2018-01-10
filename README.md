# OKRs
Objectives and Key Results tooling built with .NET Core 2.0 and C#

Work in progress...

## Setup

Make sure the following keys gets populated with correct data using VS user secrets, appsettings or env variables:
```json
{
  "DataConnectionString": "mongodb://...",
  "Database": "OKRs",
  "Authentication": {
    "Google": {
      "ClientId": "...",
      "ClientSecret": "..."
    }
  }
}
```
