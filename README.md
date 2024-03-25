# Clean architecture WebAPI template
This is a template I've made for quickly setting up new .NET Core WebAPI projects with Clean architecture. It has the following four projects:
1. Domain
2. Application
3. Infrastructure
4. WebAPI (Startup project)

#### It includes the following features and tools:
- Docker with docker-compose with a redis service included, for caching.
- Github actions workflow script to Build and Push WebAPI image to AWS ECR.
- Basic login, registration, logout and JWT refresh logic (with access and refresh tokens). There is also an included PasswordHasher implementation.
- SwaggerUI with dark theme.
- A simple database context with options for migrations.

### How to setup and start the project
In order to run the project, you will need to create a new .env file in the root folder with the following pairs:
```
ASPNETCORE_ENVIRONMENT=Development
TokenSigningKey=<key>
TokenIssuer=<issuer>
TokenAudience=<audience>
DefaultConnection=Host=<host>;Port=<port>;Username=<username>;Password=<password>;Database=<database>
RedisConnection=<ip>:6379
```

### How to perform migrations
Because we are using docker-compose with an .env file which includes the database connection string, it's not really an option to make the migrations with it. We would need to create an additional appsettings.json file in the WebAPI project, something similar to this:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "DefaultConnection": "Host=<host>;Port=<port>;Username=<username>;Password=<password>;Database=<db>",
  "AllowedHosts": "*"
}
```
Then you would need to execute the following steps:
1. Install EF Core tools, if you haven’t already `dotnet tool install --global dotnet-ef`
2. Open the package manager console and navigate to the WebAPI Project (using `cd ./WebAPI` command)
3. Run `dotnet ef migrations add <migration-name>`
4. Run `dotnet ef database update`

### How to use the Github actions workflow script (main.yml)
1. Go to your Github.com > the repository > settings > secrets and variables > Actions > Secrets tab
2. Add one new secret with following name: `AWS_ACCESS_KEY_ID` with the value from AWS.
3. Add another secret with the following name: `AWS_SECRET_ACCESS_KEY` with the value from AWS.
4. Make sure to configure your AWS ECR correctly.

If you don't want to use it, just delete the .github folder.
