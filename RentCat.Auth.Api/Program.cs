using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RentCar.IOC.Dependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//dendency
builder.Services.AddContextDependecy(builder.Configuration.GetConnectionString("RentCarContext"));
builder.Services.AddRentCarDependency();

var key = Encoding.ASCII.GetBytes(builder.Configuration["TokenInfo:SigningKey"]);

builder.Services.AddAuthentication(jb =>
    {
        jb.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        jb.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddCookie()
    .AddJwtBearer(jb =>
    {
        jb.RequireHttpsMetadata = false;
        jb.SaveToken = true;
        jb.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });


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