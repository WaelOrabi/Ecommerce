using Application.Extensions;
using Ecommerce.API.Extensions;
using Ecommerce.Application.Middleware;
using Ecommerce.Infrastructure.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddHttpContextAccessor();

builder.Services.RegisterLocalization();
builder.Services.RegisterDbContext(config);
builder.Services.RegisterRedis(config);
builder.Services.RegisterValidation();


#region Configure Authentication and  Authorization
builder.Services.RegisterAuthentication(config);

builder.Services.RegisterAuthorization();
#endregion

builder.Services.RegisterMapper();
builder.Services.RegisterUnitOfWork();
builder.Services.RegisterServices();



#region caching
//builder.Services.AddResponseCaching();
builder.Services.AddOutputCache(options =>
{
    //options.AddBasePolicy(bp => bp.Expire(TimeSpan.FromSeconds(10)));
    options.AddPolicy("120SecondsDuration", p => p.Expire(TimeSpan.FromSeconds(120)));
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ecommerce API V1"));
}

app.UseCors("CorsPolicy");
//app.UseResponseCaching();
app.UseOutputCache();

app.UseHttpsRedirection();

#region Localization Middleware
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
#endregion

app.UseMiddleware<ErrorHandlerMeddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
