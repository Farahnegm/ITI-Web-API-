
using ITI_Api.Interfaces;
using ITI_Api.Mapping;
using ITI_Api.Models;
using ITI_Api.Repository;
using Microsoft.EntityFrameworkCore;
using ITI_Api.UnitOfWorks ;
using Abp.Domain.Uow;
namespace ITI_Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddDbContext<ITIContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connectionstr")));
            builder.Services.AddAutoMapper(typeof(StudentMap),typeof(DepartmentMap), typeof(InstructorMap));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<UnitOfWork>();
       



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(op => op.SwaggerEndpoint("/openapi/v1.json", "v1"));
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            


            app.MapControllers();

            app.Run();
        }
    }
}
