using Application.Extensions;
using Ecommerce.API.Extensions;
using Ecommerce.Application.Middleware;
using Ecommerce.Infrastructure.Extensions;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();




#region configure api layer
builder.Services.AddSwaggerGenWithAuth();
builder.Services.RegisterDbContext(config);
builder.Services.RegisterRedis(config);
#endregion


#region configure application layer
builder.Services.RegisterServices();
builder.Services.RegisterAuthentication(config);
builder.Services.RegisterAuthorization();
builder.Services.RegisterMapper();
builder.Services.RegisterValidation();
builder.Services.RegisterLocalization();
builder.Services.RegisterHttpContextAccessor();
#endregion

#region configure infrastructure layer
builder.Services.RegisterationIdentity();
builder.Services.RegisterationUnitOfWork();
#endregion





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
