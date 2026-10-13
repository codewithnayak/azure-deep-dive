using FirApi.Application.Interfaces;
using FirApi.Application.Services;
using FirApi.Infrastructure.Data;
using FirApi.Infrastructure.Repositories;
using FirApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<FirDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("FirDatabase")));

builder.Services.AddScoped<IFirRepository, FirRepository>();
builder.Services.AddScoped<IFirService, FirService>();

// Configure your actual JWT bearer authentication scheme
// and issuer/audience validation here before deployment.
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();