# GameStore

Game Store is a project that I'll be using as a template to future consults. The idea is creat a project that simulate a corporative project, but testing new technologies

## Usage

The program's use will be very simple, it will be a Game Storage where you can consult or buy games

## Installation

before you try to test the project you'll need to run a image of your database (update the string connection if you don't use the example), for example:
```bash
docker run --name containerSQLServer -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=admin123!" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2017-latest
```
install the packages necessarys to run your migrations:
```bash
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet tool install --global dotnet-ef
```
run and create your database:
```bash
dotnet ef migrations add <YourMigrationName>
dotnet ef database update
```

## Roadmap

here the tecnologies that will be implemented:

Microsoft:
- dotnet 6.0 and csharp 10 (ok)
- Entity FrameWorkCore (ok, but maybe to try hard on SQL I switch to Dapper)
- Identity (ok)
- JWT (implemented)
- Razor Page (but in future the idea is change to SPA)
- git (ok, as you see here)
- microsoft sql server (ok, using as docker image)

Others:
- Swagger
- Docker
- kubernetes
- rancher
- rabbitMQ
- cookies
- Angular
- xunit
- zabbix (as alternative to datadog, but maybe not)
- Jenkins or Travis CI (probably Jenkins, because I never used)

# Authors and acknowledgment
I'm the one whom is coding, but the knowledgement will come from over the internet

## License
[MIT](https://choosealicense.com/licenses/mit/)

## Project status
the thing will run depend of what I have to do, somedays I'll rush and others I'll slow down the code

# Changelog
## [1.0.0] - 2022-03-25
### Added
- Included a API for user authentication using aspnetcore Identity


