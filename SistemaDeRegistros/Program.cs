using Microsoft.EntityFrameworkCore;
using SistemaDeRegistros.Data;
using SistemaDeRegistros.Repositorios;
using SistemaDeRegistros.Repositorios.Interfaces;

namespace SistemaDeRegistros
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Use InMemory database instead of SQL Server
            builder.Services.AddDbContext<SistemaTarefasDBContext>(options =>
                options.UseInMemoryDatabase("SistemaDeRegistrosInMemory"));

            builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

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
