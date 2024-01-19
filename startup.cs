using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using prueba.filters;
using prueba.IhostedService;
using prueba.middleware;
using prueba.services;

namespace prueba
{
    public class startUp
    {
        public startUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                // Para agregar filtros de manera global
                options =>
                {
                    options.Filters.Add(typeof(MyExceptionFilter));
                }
            ).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddDbContext<AplicationDBContex>(options => options.UseNpgsql(Configuration.GetConnectionString("PostSqlConnection")));
            // services.AddSingleton<getRandomNum>();
            // services.AddTransient<getRandomNum>();
            services.AddTransient<MyFilterAction>();
            services.AddTransient<getRandomNum>();
            services.AddHostedService<MyHostedService>();
            services.AddResponseCaching();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Al usar run solo se ejecuta un middleware
            // app.Run(async context =>
            // {
            //     await context.Response.WriteAsync("Hello from 2nd delegate.");
            // });

            //Al usar map , decimos en que rutas se van a ejecutar el middleware
            // app.Map("/ruta", app =>
            // {
            //     app.Run(async context =>
            //     {
            //         await context.Response.WriteAsync("Hello from 2nd delegate.");
            //     });

            // });
            // app.Use(async (context, next) =>
            // {
            //     logger.LogInformation("Middelware 1");
            //     await next();
            //     logger.LogInformation("Middelware 1");
            // });
            // Configure the HTTP request pipeline.
            //corremos nuestro middleware
            app.UseLogsResponse();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseResponseCaching();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}