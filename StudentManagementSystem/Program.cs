using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace StudentManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddMvc();
            builder.Services.AddSession();
            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters()
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidAudience = builder.Configuration["Jwt:Audience"],
            //        ValidIssuer = builder.Configuration["Jwt:Issuer"],
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            //    };
            //});

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseAuthentication();
            app.UseAuthorization();
            //app.UseMvcWithDefaultRoute();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
