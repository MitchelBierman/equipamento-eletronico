using EquipamentoEletronico.Application.DependencyInjection;
using EquipamentoEletronico.Domain.Entities;
using EquipamentoEletronico.Infrastructure;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<EquipamentoEletronicoValidator>();

builder.Services.AddDbContext<EquipamentoEletronicoDbContext>(options =>
    options.UseInMemoryDatabase("EquipamentosDB"));

builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddServices(builder.Configuration);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Equipamento API",
        Version = "v1"
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Equipamento API v1");
    });
}

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