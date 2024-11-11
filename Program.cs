using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
using ServiMun.Models;
using ServiMun.Repository;
using ServiMun.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<TributoMunicipalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptionsAction : sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null
            );
         }
    ));

// Interfaces Repository

builder.Services.AddScoped<IRepositoryResult<TributoMunicipal>, TributoMunicipalRepository>();
builder.Services.AddScoped<IContribuyenteRepository, ContribuyenteRepository>();
builder.Services.AddScoped<IPadronContribuyenteRepository, PadronContribuyenteRepository>();
builder.Services.AddScoped<IPadronBoletaRepository, PadronBoletaRepository>();

builder.Services.AddScoped<IRepositoryResult<Servicio>,ServicioRepository>();
builder.Services.AddScoped<IServicioClienteRepository, ServicioClienteRepository>();
builder.Services.AddScoped<IServicioBoletaRepository, ServicioBoletaRepository>();

builder.Services.AddScoped<IRepositoryResult<ComprobanteControl>, ComprobanteRepository>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();

// Interfaces Service

builder.Services.AddScoped<ITributoMunicipalService, TributoMunicipalService>();
builder.Services.AddScoped<IContribuyenteService, ContribuyenteService>();
builder.Services.AddScoped<IPadronContribuyenteService, PadronContribuyenteService>();
builder.Services.AddScoped<IPadronBoletaService, PadronBoletaService>();

builder.Services.AddScoped<IServicioService, ServicioService>();
builder.Services.AddScoped<IServicioClienteService, ServicioClienteService>();
builder.Services.AddScoped<IServicioBoletaService, ServicioBoletaService>();

builder.Services.AddScoped<IComprobanteService, ComprobanteService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();

builder.Services.AddScoped<IPagoService, PagoService>();

// Fin interfaces

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
