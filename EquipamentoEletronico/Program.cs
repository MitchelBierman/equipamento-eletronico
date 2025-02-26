using EquipamentoEletronico.Application.DependencyInjection;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<EquipamentoEletronicoValidator>();

builder.Services.AddDbContext<EquipamentoEletronicoDbContext>(options =>
    options.UseInMemoryDatabase("EquipamentosDB"));

builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddServices(builder.Configuration);

var app = builder.Build();

app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EquipamentoEletronicoDbContext>();

    if (!context.Equipamentos.Any())
    {
        context.Equipamentos.AddRange(
            new Equipamento("Monitor Philco 299Y", "Monitor", 2, new DateTime(2025, 01, 09)),
            new Equipamento("Impressora Phillips 10Z", "Impressora", 3, new DateTime(2024, 12, 15)),
            new Equipamento("Mouse Redragon 8000M", "Mouse", 0, new DateTime(2025, 02, 15))
        );

        context.SaveChanges();
    }
}

app.Run();