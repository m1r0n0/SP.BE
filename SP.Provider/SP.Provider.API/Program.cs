using Microsoft.EntityFrameworkCore;
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
