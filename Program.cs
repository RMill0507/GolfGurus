using MySql.Data.MySqlClient;
using System.Data;

namespace GolfGurus
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // creates an instance of WebApplicationBuilder and passing the command-line arguments
            var builder = WebApplication.CreateBuilder(args);

            // adding controllers and views to the application and this service is required for the MVC framework to work
            builder.Services.AddControllersWithViews();
            // adding a scoped service, which means that a new instance of IDbConnection will be created for each scope
            // it will use MySqlConnection and the connection string from configuration file
            builder.Services.AddScoped<IDbConnection>((s) =>
            {
                IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("savedcourses"));
                conn.Open();
                return conn;
            });
            
            //adding a transient service, which means that a new instance of APIClient will be created each time they are requested
            builder.Services.AddTransient<APIClient>();
            
            // adding a transient service, which means that a new instance of IGolfCourseRepository will be created 
            // each time they are requested and it will implement the GolfCourseRepository class
            builder.Services.AddTransient<IGolfCourseRepository, GolfCourseRepository>();

            // build the web application based on the services and configurations provided
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