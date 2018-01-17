# OKRs
Objectives and Key Results tooling built with .NET Core 2.0 and C#

Work in progress...

## Setup

Make sure the following keys gets populated with correct data using VS user secrets, appsettings or env variables:
```json
{
  "Database": {
    "ConnectionString": "mongodb://",
    "HostUrl": "https://{dbaccount}.documents.azure.com/",
    "Password": "",
    "Name": "OKRs",
    "UserCollection": "identities",
    "ObjectivesCollection": "objectives"
  },
  "Authentication": {
    "Google": {
      "ClientId": "",
      "ClientSecret": ""
    }
  }
}
```
