using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VBOOK2.Backend.Data;
using VBOOK2.Backend.Hubs; // Ensure you include the namespace for your ChatHub

namespace VBOOK2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure Entity Framework with SQL Server
            builder.Services.AddDbContext<ProfileBookContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("ProfileBookConnection")));

            // Enable CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Configure JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // Add SignalR
            builder.Services.AddSignalR();

            // Configure Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "VBOOK2 API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // Show detailed errors in development
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VBOOK2 V1");
                    c.RoutePrefix = string.Empty; // Makes Swagger UI the default page
                });
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll"); // Use the CORS policy
            app.UseAuthentication(); // Enable JWT Authentication
            app.UseAuthorization();

            // Map controllers and SignalR hubs
            app.MapControllers();
            app.MapHub<ChatHub>("/chathub"); // Ensure this is after building the app

            app.Run();
        }
    }
}
