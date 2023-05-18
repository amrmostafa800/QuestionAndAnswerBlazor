using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestionAndAnswerBlazor.Services;
using System.Text;

namespace QuestionAndAnswerBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Key").Value ?? throw new InvalidOperationException("JWT string 'Key' not found.")))
                    };
                });

            var connectionString = builder.Configuration.GetConnectionString("Default") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<Models.AppContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddTransient<AccountsService>();
            builder.Services.AddTransient<QuestionsService>();
            builder.Services.AddTransient<AnswerService>();
            builder.Services.AddTransient<CommentsService>();


            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMvc();
            builder.Services.AddControllersWithViews();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme 
                        {
                            Reference = new OpenApiReference 
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer" 
                            },
                            Name = "Bearer",
                            In = ParameterLocation.Header 
                        },
                        new List<string>() 
                    } 
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.MapControllers();

            app.Run();
        }
    }
}