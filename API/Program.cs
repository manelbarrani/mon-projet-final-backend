using API.Extension;
using Application.Mappings;
using MediatR;
using Application.Interfaces;
using Persistance.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuration
IConfiguration configuration = builder.Configuration;

// Contexte et Repositories
builder.Services.ConfigureContext(configuration);
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// MediatR, AutoMapper
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

// Contrôleurs
builder.Services.AddControllers();

// Services additionnels
builder.Services.RetryExtension(configuration);
builder.Services.AddMemoryCache();
builder.Services.AddSignalR();

// CORS
var allowedOrigins = new[]
{
    "http://localhost:4200",
    "https://ftusa-web.dev2.addinn-group.com"
};

builder.Services.AddCors(options =>
{
    options.AddPolicy("cors", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// ? Swagger configuration claire
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Produit Web Api",
        Version = "v1",
        Description = "API de gestion des produits (CRUD)"
    });

    // Pour regrouper les endpoints sous le nom du controller
    options.TagActionsBy(api =>
    {
        return new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] };
    });

    options.DocInclusionPredicate((docName, apiDesc) => true);
});

var app = builder.Build();

// Middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Produit Web Api v1");
        c.DocumentTitle = "Documentation API Produit";
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("cors");
app.UseAuthorization();

// Contrôleurs
app.MapControllers();

app.Run();
