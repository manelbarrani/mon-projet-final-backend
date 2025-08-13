using API.Extension;
using Application.Interfaces;
using Application.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore; // nécessaire pour Migrate()
using Microsoft.OpenApi.Models;
using Persistance.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configuration
IConfiguration configuration = builder.Configuration;

// Contexte et Repositories
builder.Services.ConfigureContext(configuration); // Assurez-vous que ConfigureContext configure bien DematContext
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

// Swagger configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Produit Web Api",
        Version = "v1",
        Description = "API de gestion des produits (CRUD)"
    });

    options.TagActionsBy(api =>
    {
        return new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"] };
    });

    options.DocInclusionPredicate((docName, apiDesc) => true);
});

var app = builder.Build();

// Appliquer les migrations automatiquement pour DematContext
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Persistance.Data.DematContext>();
    db.Database.Migrate();
}

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
