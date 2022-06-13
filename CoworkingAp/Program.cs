using System;
using Microsoft.EntityFrameworkCore;
using CoworkingAp.Data;


var builder = WebApplication.CreateBuilder(args);

builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApiContext>
(opt => opt.UseInMemoryDatabase("DesksDb"));

// Add services to the container.

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



