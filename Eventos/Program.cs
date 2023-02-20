using AutoMapper;
using Evento.Service.Interface;
using Evento.Service.Service;
using Eventos.Infra.Data.Repository;
using Eventos.Service.DTO;
using Eventos.Service.Entity;
using Eventos.Service.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var chaveCripto = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("SECRET_KEY"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(chaveCripto),
                    ValidateIssuer = true,
                    ValidIssuer = "APIClientes.com",
                    ValidateAudience = true,
                    ValidAudience = "APIEvents.com"
                };
            });
builder.Services.AddScoped<ICityEventService, CityEventService>();
builder.Services.AddScoped<IEventReservationService, EventReservationService>();
builder.Services.AddScoped<ICityEventRepository, CityEventRepository>();
builder.Services.AddScoped<IEventReservationRepository, EventReservationRepository>();


MapperConfiguration mapConfig = new(map =>
{
    map.CreateMap<CityEventEntity, CityEventDTO>().ReverseMap();

    map.CreateMap<EventReservationEntity, EventReservationDTO>().ReverseMap();
});

IMapper mapp = mapConfig.CreateMapper();

builder.Services.AddSingleton(mapp);

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
