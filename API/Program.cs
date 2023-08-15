using API.Extension;
using MediatR;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;

builder.Services.ConfigureContext(configuration);
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("cors", builder =>
{
    builder
    //.WithOrigins("http://localhost:4200", "https://ftusa-web.dev2.addinn-group.com")
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
}));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.ConfigureSwagger();

var app = builder.Build();
app.UseRouting();
// Configure the HTTP request pipeline.
app.UseCors("cors");
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
