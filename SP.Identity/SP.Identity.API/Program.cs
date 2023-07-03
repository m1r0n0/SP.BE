using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP.Identity.API.MappingProfiles;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.BusinessLayer.Services;
using SP.Identity.DataAccessLayer.Data;
using SP.Identity.DataAccessLayer.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(cfg => { cfg.User.RequireUniqueEmail = true;})
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(AppUserMappingProfile));

builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    cfg.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
