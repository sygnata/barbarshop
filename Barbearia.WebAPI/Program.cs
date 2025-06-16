using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;
using Barbearia.Infrastructure.Persistence.ValueConverters;
using Barbearia.Infrastructure.Serializers;
using Barbearia.Infrastructure.Swagger;
using Barbearia.WebAPI.Middlewares;
using DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BarbeariaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
        options.JsonSerializerOptions.Converters.Add(new DateTimeUtcConverter());
        options.JsonSerializerOptions.Converters.Add(new ValueObjectJsonConverter<NomeFantasia, string>());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Barbearia SaaS API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token JWT no campo abaixo.\n\nExemplo: {seu token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    c.SchemaFilter<TimeSpanSchemaFilter>();
    c.SchemaFilter<ValueObjectSchemaFilter>();

});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthorization();
app.UseWhen(context => context.User.Identity.IsAuthenticated, appBuilder =>
{
    appBuilder.UseMiddleware<TenantMiddleware>();
});
app.MapControllers();

app.Run();
app.Run();