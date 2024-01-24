using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using prueba.services;

namespace prueba
{
    public class startUp
    {
        public startUp(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(
                // Para agregar filtros de manera global
                options =>
                {
                    // options.Filters.Add(typeof(MyExceptionFilter));
                }
            ).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson(
            options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }

            );

            services.AddDbContext<AplicationDBContex>(options => options.UseNpgsql(Configuration.GetConnectionString("PostSqlConnection")));
            services.AddTransient<hashService>();

            services.AddCors(options =>
                options.AddDefaultPolicy(
                    builder => builder.WithOrigins("https//localhost:1234").AllowAnyMethod().AllowAnyHeader())
            );
            services.AddResponseCaching();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                option => option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["keyJwt"])
                    ),
                    ClockSkew = TimeSpan.Zero
                }
            );
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
                {
                    c.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header
                    }
                    );
                    c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                        {
                            new OpenApiSecurityScheme{
                                Reference= new OpenApiReference{
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new string[]{}
                        }

                    });
                }
            );
            //Para agregar el automapper
            services.AddAutoMapper(typeof(startUp));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AplicationDBContex>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(
                options => options.AddPolicy("isAdmin", policy => policy.RequireClaim("isAdmin"))
            );
            //Agregar servicios de encriptacion
            services.AddDataProtection();
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

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseResponseCaching();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}