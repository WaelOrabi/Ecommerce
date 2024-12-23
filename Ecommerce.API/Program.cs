using Application.Extensions;
using Ecommerce.Application.Authorization;
using Ecommerce.Application.Extensions;
using Ecommerce.Infrastructure.Database;
using Ecommerce.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
// Configure Authentication and JWT Bearer

builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = config["Jwt:Issuer"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:SigningKey"])),
            ValidateAudience = true,
            ValidAudience = config["Jwt:Audience"],
            ClockSkew=TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options => {
    options.AddPolicy("SuperUsersOnly", builder =>
    {
        builder.RequireRole("SuperUser");
       
    });
    options.AddPolicy("phoneNumber", builder => {
        builder.RequireClaim("phoneNumber", "0981078432");
    });
    options.AddPolicy("AgeGreaterThan25", builder =>
    {
        builder.RequireAssertion(context =>
        {
            var birthDateClaim = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth);
            if (birthDateClaim == null)
            {
                return false; 
            }

            if (!DateTime.TryParse(birthDateClaim.Value, out var birthDate))
            {
                return false; 
            }

            return DateTime.Today.Year - birthDate.Year >= 25; 
        });
    });
});


// Configure DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

// Register custom services
builder.Services.RegisterUnitOfWork();
builder.Services.RegisterServices();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<PermissionBasedAuthorizationFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API V1"));
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
