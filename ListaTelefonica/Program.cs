using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ListaTelefonica.Data;
using ListaTelefonica.Repositories.Interfaces;
using ListaTelefonica.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();

builder.Services.AddDbContext<AgendaDbContext>(options =>
    options.UseSqlite(configuration.GetSection("DbConnectionString").Value));

builder.Services.AddScoped<IContatoRepository, ContatoRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();