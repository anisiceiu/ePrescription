
using AutoMapper;
using ePerscription.Application.DTOs;
using ePerscription.Application.Interfaces;
using ePerscription.Application.Services;
using ePerscription.Domain.Interfaces;
using ePerscription.Infrastructure.Data;
using ePerscription.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ePerscription.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // AutoMapper
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DrugDto, Domain.Entities.Drug>().ReverseMap();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);

            // Dependency Injection

            builder.Services.AddScoped<IDrugRepository, DrugRepository>();
            builder.Services.AddScoped<IDrugService, DrugService>();
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
