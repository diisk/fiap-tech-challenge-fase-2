using API.Middlewares;
using Application.DTOs.AreaDtos;
using Application.DTOs.Auth;
using Application.DTOs.ContatoDtos;
using Application.Interfaces;
using Application.Mappers;
using Application.Mappers.AreaMappers;
using Application.Mappers.ContatoMappers;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.AreaInterfaces;
using Domain.Interfaces.ContatoInterfaces;
using Domain.Interfaces.UsuarioInterfaces;
using Infrastructure.DbContexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using System.Net;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddDbContext<OnlyReadDbContext>(options =>
{
    DbContextOptionsConfigurator.Configure(options);
}, ServiceLifetime.Scoped);

builder.Services.AddDbContext<OnlyWriteDbContext>(options =>
{
    DbContextOptionsConfigurator.Configure(options);
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAreaRepository, AreaRepository>();
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();


builder.Services.AddTransient<IResponseService, ResponseService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<IContatoService, ContatoService>();

builder.Services.AddScoped<AreaToAreaResponseMapper>();

builder.Services.AddAutoMapper(typeof(CustomMapper<RegistrarRequest, Usuario>));
var mapperConfig = new MapperConfiguration(cfg =>
{
    AreaToAreaResponseMapper.ConfigureMapping(cfg, builder.Services);
    AtualizarContatoRequestToContatoMapper.ConfigureMapping(cfg, builder.Services);


    cfg.CreateMap<RegistrarRequest, Usuario>();
    cfg.CreateMap<NovaAreaRequest, Area>();
    cfg.CreateMap<CadastrarContatoRequest, Contato>();
    cfg.CreateMap<Contato, ContatoResponse>();
});

IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

var key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("SecretJWT")!);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        x.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                return ResponseService.GetPatterResponse(
                    context.HttpContext,
                    HttpStatusCode.Unauthorized,
                    "Voc� n�o tem autoriza��o para acessar este recurso.");
            },
            OnForbidden = context =>
            {
                return ResponseService.GetPatterResponse(
                    context.HttpContext,
                    HttpStatusCode.Forbidden,
                    "Voc� n�o tem permiss�o para acessar este recurso.");
            }
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(x =>
{
    var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
    x.IncludeXmlComments(xmlPath);

    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMetricServer();
app.UseHttpMetrics();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();

app.Run();
