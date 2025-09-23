
using AutoMapper;
using ePerscription.Api.Configuration;
using ePerscription.Application.DTOs;
using ePerscription.Application.Interfaces;
using ePerscription.Application.Services;
using ePerscription.Domain.Interfaces;
using ePerscription.Infrastructure.Data;
using ePerscription.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ePerscription.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var appSettingsSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(appSettingsSection);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:4200") // Specify the URL of your Angular app
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials();
                    });
            });

            builder.Services
                            .AddAuthorization()
                            .AddAuthentication(x =>
                            {
                                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                            })
                            .AddJwtBearer(x =>
                            {
                                x.RequireHttpsMetadata = false;
                                x.SaveToken = true;
                                x.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuerSigningKey = true,
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettingsSection.Get<AppSettings>()?.Secret ?? throw new InvalidOperationException("Secret is not configured."))),
                                    ValidateIssuer = false,
                                    ValidateAudience = false
                                };
                            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugDto, Domain.Entities.Drug>().ReverseMap();
                cfg.CreateMap<UserDto, Domain.Entities.User>().ReverseMap();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Dependency Injection

            builder.Services.AddScoped<IDrugRepository, DrugRepository>();
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<IDrugService, DrugService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            // DB Context
            builder.Services.AddDbContext<EPrescriptionContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors("AllowSpecificOrigin");


            app.MapControllers();

            app.Run();
        }
    }
}
