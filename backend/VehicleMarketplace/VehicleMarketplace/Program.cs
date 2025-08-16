using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using VehicleMarketplace.Dao;
using VehicleMarketplace.Data;
using VehicleMarketplace.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddScoped<UserAccountService>();
builder.Services.AddScoped<UserAccountDAO>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<CarDAO>();
builder.Services.AddScoped<MotorcycleService>();
builder.Services.AddScoped<MotorcycleDAO>();
builder.Services.AddScoped<MakeService>();
builder.Services.AddScoped<MakeDAO>();

// builder.Services.AddSingleton<UserAccountService>();

builder.Services.AddDbContext<VehicleMarketplaceDbContext>(options => {
        options.UseMySQL(builder.Configuration.GetConnectionString("MyDefaultTutorialConnectionString"));
    }
);

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Activate CORS before MapGet/MapControllers and Authentication
app.UseCors("AllowAngular");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
