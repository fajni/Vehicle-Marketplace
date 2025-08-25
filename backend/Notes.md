# Backend for Vehicle Marketplace

- [Database connection](#database-connection)
- [Migrations](#migrations)
    - [Problems](#problems)
- [Cookie and Cors settings](#cookie-and-cors-settings)
- [Endpoints](#endpoints)

<hr/>

<p align="center">
<img src="../images/database/er.png"/>
</p>

## Database connection

To connect MySQL db to the asp .net core application we have to install certain Nuget packages:

- Microsoft.EntityFrameworkCore
- MySql.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Design - for migrations

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",

  "ConnectionStrings": {
    "MyDefaultTutorialConnectionString": "server=localhost;port=3306;user=admin;password=admin;database=posts"
  }
}

```

- In _Program.cs_ add connection string before build:

```csharp
builder.Services.AddDbContext<PostsDbContext>(options => {
        options.UseMySQL(builder.Configuration.GetConnectionString("MyDefaultTutorialConnectionString"));
    }
);
```

## Migrations

To open Terminal: `View->Terminal`

Locate yourself in root folder and run command for migration (_where Program.cs file is_):

- from _Package Manager Console_: `Add-Migration YourMigrationName`, `Update-Database`
- from _Powershell_: `dotnet ef migrations add YourMigrationName`, `dotnet ef database update`

Install _dotnet tool_: `dotnet tool install --global dotnet-ef`

### Problems

Check packages/dependencies:

- from _{YourProject}.csproj_ file (every package should be in this file, with their version):

```html
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net9.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.7" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.7" />
    <PackageReference Include="MySql.EntityFrameworkCore" Version="9.0.6" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.7" />
  </ItemGroup>

</Project>
```

## Cookie and CORS settings

- Program.cs

```csharp
// ...

// Add cookie, configure cookie so that browser automatically saves it
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => {
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/accessdenied";

    options.Cookie.Name = ".AspNetCore.Cookies"; // you value for cookie name
    options.Cookie.HttpOnly = true; // only backend can read
    options.Cookie.SameSite = SameSiteMode.None; // required for cross-site
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // set always on https
});

// Add CORS Service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Angular dev server
                                                        //.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials(); // for cookie (allow backend to send cookie)
        }
    );

    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
    );

});

// ...

// Activate CORS before MapGet/MapControllers and Authentication
app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

// ... 
```

## Endpoints

### User Accounts

|  Request   | Link                      | Description                     |Role        |
|------------|---------------------------|---------------------------------|------------|
| GET        | /api/users                | Get all registered users        |Admin, User |
| GET        | /api/users/{id}           | Get user by id                  |Admin, User |
| POST       | /api/users/registration   | Add new user; registration      |/           |
| DELETE     | /api/users/delete/{id}    | Delete user by id               |Admin, User |
| PUT        | /api/users/update/{id}    | Update user                     |Admin, User |
| POST       | /api/users/login          | Login                           |/           |
| GET        | /api/users/logout         | Logout                          |Admin, User |

### Cars

|  Request   | Link                      | Description                     |Role        |
|------------|---------------------------|---------------------------------|------------|
| GET        | /api/cars                 | Get all available cars          |/           |
| GET        | /api/cars/{vin}           | Get car by vin                  |/           |
| POST       | /api/cars/add             | Add new car                     |Admin, User |
| DELETE     | /api/cars/delete/{vin}    | Delete car by vin               |Admin, User |
| PUT        | /api/cars/update/{vin}    | Update car                      |Admin, User |

### Motorcycles

/api/motorcycles = /api/motors

|  Request   | Link                          | Description                     |Role        |
|------------|-------------------------------|---------------------------------|------------|
| GET        | /api/motorcycles              | Get all available motorcycles   |/           |
| GET        | /api/motorcycles/{vin}        | Get motorcycles by vin          |/           |
| POST       | /api/motorcycles/add          | Add new motorcycles             |Admin, User |
| DELETE     | /api/motorcycles/delete/{vin} | Delete motorcycles by vin       |Admin, User |
| PUT        | /api/motorcycles/update/{vin} | Update motorcycles              |Admin, User |

### Makes

|  Request   | Link                      | Description                     |Role        |
|------------|---------------------------|---------------------------------|------------|
| GET        | /api/makes                | Get all available makes         |/           |
| GET        | /api/makes/{id}           | Get make by id                  |/           |
| POST       | /api/makes/add            | Add new make                    |Admin       |
| DELETE     | /api/makes/delete/{id}    | Delete make by id               |Admin       |
| PUT        | /api/makes/update/{id}    | Update make                     |Admin       |

### Imagesc

