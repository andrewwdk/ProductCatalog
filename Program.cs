using Microsoft.EntityFrameworkCore;
using ProductCatalog.DbContexts;
using ProductCatalog.Mapper;
using ProductCatalog.Middlewares;
using ProductCatalog.Repository;
using ProductCatalog.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfiguration) =>
{
    loggerConfiguration.WriteTo.Console();
    loggerConfiguration.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProductDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("ProductDb");
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

MapsterConfig.RegisterMappings();

var app = builder.Build();

app.UseMiddleware();

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
