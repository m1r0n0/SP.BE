using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SP.Provider.API.MappingProfiles;
using SP.Provider.BusinessLayer.Interfaces;
using SP.Provider.BusinessLayer.Services;
using SP.Provider.DataAccessLayer.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ProviderContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(ProviderMappingProfile));

builder.Services.AddScoped<IProviderService, ProviderService>();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SP.Provider API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
