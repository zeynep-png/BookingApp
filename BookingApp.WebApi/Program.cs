using BookingApp.Business.DataProtection;
using BookingApp.Business.Operations.Feature;
using BookingApp.Business.Operations.Hotel.Dtos;
using BookingApp.Business.Operations.Setting;
using BookingApp.Business.Operations.User;
using BookingApp.Data.Context;
using BookingApp.Data.Entities;
using BookingApp.Data.Repositories;
using BookingApp.Data.UnifOfWork;
using BookingApp.WebApi.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // Register the controllers

// Configure Swagger/OpenAPI for documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer", // Authentication scheme
        BearerFormat = "Jwt", // Format of the token
        Name = "Jwt Authentication", // Name for the security definition
        In = ParameterLocation.Header, // Location of the token
        Type = SecuritySchemeType.Http, // Type of security
        Description = "Put **_ONLY_** your JWT Bearer Token on Textbox below", // Description for users

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme, // Reference ID
            Type = ReferenceType.SecurityScheme // Reference type
        }
    };
    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {jwtSecurityScheme, Array.Empty<string>() } // Require the JWT security scheme
    });
});

// Add Data Protection service
builder.Services.AddScoped<IDataProtection, DataProtection>();

// Set the directory for key storage
var keysDirectory = new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "App_Data", "Keys"));
builder.Services.AddDataProtection()
    .SetApplicationName("BookingApp") // Application name for data protection
    .PersistKeysToFileSystem(keysDirectory); // Persist keys to file system

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Validate the token issuer
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Valid issuer from configuration

            ValidateAudience = true, // Validate the audience
            ValidAudience = builder.Configuration["Jwt:Audience"], // Valid audience from configuration

            ValidateLifetime = true, // Validate the token lifetime

            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!)) // Signing key for validation
        };
    });

// Get the connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("default");

// Configure the DbContext for SQL Server
builder.Services.AddDbContext<BookingAppDbContext>(options => options.UseSqlServer(connectionString));

// Register generic repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register services for user, feature, hotel, and setting management
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IFeatureService, FeatureManager>();
builder.Services.AddScoped<IHotelService, HotelManager>();
builder.Services.AddScoped<ISettingService, SettingManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger in development
    app.UseSwaggerUI(); // Enable Swagger UI
}

app.UseMaintenanceMode(); // Use the maintenance mode middleware

app.UseHttpsRedirection(); // Redirect HTTP to HTTPS

app.UseAuthentication(); // Enable authentication

app.UseAuthorization(); // Enable authorization

app.MapControllers(); // Map the controllers

app.Run(); // Run the application
