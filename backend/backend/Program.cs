using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Repository.Irepository;
using backend.Repository;
using backend.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(p=>p.AddPolicy("corspolicy", build =>
            {
                build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            } ));

            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.SaveToken = true;
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });

            

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddSingleton<Ilogging, logging>();

            builder.Services.AddDbContext<ApplicationDbContext>(
            option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            }
            );

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseCors("corspolicy");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();


            /*if (app.Environment.IsDevelopment())
            {
                app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
                    string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
            }*/


            app.MapControllers();

            app.Run();
        }
    }
}