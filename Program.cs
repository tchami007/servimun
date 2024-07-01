using Microsoft.EntityFrameworkCore;
using ServiMun.Data;
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

builder.Services.AddScoped<ITributoMunicipalService, TributoMunicipalService>();
builder.Services.AddScoped<IContribuyenteService, ContribuyenteService>();
builder.Services.AddScoped<IPadronContribuyenteService, PadronContribuyenteService>();
builder.Services.AddScoped<IPadronBoletaService, PadronBoletaService>();



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
