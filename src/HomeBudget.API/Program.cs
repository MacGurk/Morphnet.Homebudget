using System.Reflection;
using HomeBudget.API.DbContexts;
using HomeBudget.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => { options.ReturnHttpNotAcceptable = true; });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

    setupAction.IncludeXmlComments(xmlCommentsFullPath);

    setupAction.AddSecurityDefinition("HomeBudgetApiBearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Description = "Input a valid token to access this API",
        In = ParameterLocation.Query,
        Scheme = "Bearer"
    });
    setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "HomeBudgetApiBearerAuth"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddDbContext<HomeBudgetContext>(options =>
{
    var databaseType = builder.Configuration["Database:Type"];
    var connectionString = builder.Configuration["Database:ConnectionString"];

    if (databaseType.IsNullOrEmpty() || connectionString.IsNullOrEmpty())
    {
        throw new ArgumentNullException($"Please configure Type and ConnectionString of Database in appsettings");
    }

    if (databaseType == "sqlite")
    {
        options.UseSqlite(connectionString);
    }
    else if (databaseType == "mysql")
    {
        options.UseMySQL(connectionString!);
    }
    else
    {
        throw new ArgumentException($"{databaseType} is not a valid database type.");
    }
});

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITransactionRepository, TransactionRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();