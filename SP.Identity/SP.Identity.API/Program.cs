using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SP.Identity.API.MappingProfiles;
using SP.Identity.BusinessLayer.Interfaces;
using SP.Identity.BusinessLayer.Services;
using SP.Identity.DataAccessLayer.Data;
using SP.Identity.DataAccessLayer.Models;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using NLog.Config;
using NLog.Targets;
using NLog;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>(cfg => { cfg.User.RequireUniqueEmail = true;})
    .AddEntityFrameworkStores<IdentityContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAutoMapper(typeof(UserMappingProfile));

builder.Services.AddScoped<IIdentityService, IdentityService>();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .WithOrigins("https://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();

        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("identity_v1", new OpenApiInfo { Title = "SP.Identity API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

var config = new LoggingConfiguration();
/*var consoleTarget = new ConsoleTarget
{
    Name = "console",
    Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}",
};*/
var fileTarget = new FileTarget
{
    Name = "log_file",
    Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}",
    FileName = "${specialfolder:folder=commonapplicationdata}/company gmbh/${appname}/logs/${appname}.log",
    /*FileName = "C:/logs/${appname}.log",*/
    KeepFileOpen = false
};
config.AddRule(NLog.LogLevel.Debug, NLog.LogLevel.Fatal, fileTarget, "*");
LogManager.Configuration = config;

/*
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@".\AppData"))
    .ProtectKeysWithCertificate(GetCertificate());

X509Certificate2 GetCertificate()
{
    var assembly = typeof(Program).GetTypeInfo().Assembly;

    var resource = assembly.GetManifestResourceNames()
        .First(x => x.EndsWith("MyCertificate.pfx"));

    using (var stream = assembly.GetManifestResourceStream(resource))
    {
        if (stream == null)
            throw new ArgumentNullException(nameof(stream));

        var bytes = new byte[stream.Length];
        stream.Read(bytes, 0, bytes.Length);
        return new X509Certificate2(bytes);
    }
}*/

var app = builder.Build();


//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/identity_v1/swagger.json", "Identity Api"));
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);

app.Run();
