using ApiTeste.Data;
using ApiTeste.Models;
using ApiTeste.Repositories;
using ApiTeste.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(obj => obj.UseMySql(builder.Configuration.GetConnectionString("conexaoMySQL")
    , ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("conexaoMySQL"))));

builder.Services.AddScoped<PessoaService>();
builder.Services.AddScoped<PessoaRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
