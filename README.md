# OKRs

Objectives and Key Results tooling built with .NET Core 2.2 and C#

Work in progress...

## Setup

Make sure the following keys gets populated with correct data using VS user secrets, appsettings or env variables:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "",
      "ClientSecret": "",
      "DomainFilter": "localhost.se" //only allow users with email domain "localhost.se" to register using Google OAuth
```

## List of potential future features

### Prioritized

* [ ] Implement Role based user/admin access
* [ ] Limit add/edit of other user's OKRs for admins
* [ ] Add docker image definition

### Nice to have

See <https://github.com/sebnor/okrs/issues>