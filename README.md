# OKRs

Objectives and Key Results tooling built with .NET Core 3.1 and C#

Work in progress...

## Setup

Make sure the following keys gets populated with correct data using VS user secrets, appsettings or env variables:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "", //Register a new Oauth2 app using https://console.developers.google.com
      "ClientSecret": "",
      "DomainFilter": "localhost.se" //Optional; Only allow users with email domain "localhost.se" to register using Google OAuth
	}
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost;Initial Catalog=okrs;Integrated Security=True;", //SQL Server
  }
}
```

### Let Entity framework apply the database schema scripts
`dotnet ef database update --context ObjectivesDbContext`
`dotnet ef database update --context ApplicationDbContext`

## List of potential future features

### Prioritized
* [ ] Vue.js based UI + turn backend into proper WebAPI
* [ ] Implement Role based user/admin access
* [ ] Limit add/edit of other user's OKRs for admins
* [ ] Add docker image definition

### Nice to have
* [ ] Apply EF migration on runtime if app is hosted in Developer mode: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/#apply-migrations-at-runtime

For more:
See <https://github.com/sebnor/okrs/issues>