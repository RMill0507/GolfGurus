using MySql.Data.MySqlClient;
using System.Data;

namespace GolfGurus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IDbConnection>((s) =>
            {
                IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("savedcourses"));
                conn.Open();//creating connection using connection string thats pulling info from savedcourses. to generate IDBconnection
                            //
                return conn;
            });
            builder.Services.AddTransient<APIClient>();//dependency injection. passing in to controller
            builder.Services.AddTransient<IGolfCourseRepository, GolfCourseRepository>();//passing idbConnection to repository

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}