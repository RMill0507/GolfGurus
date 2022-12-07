using GolfGurus.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfGurus.Controllers
{
    public class CoursesController : Controller
    {
        private readonly APIClient _client;
        public CoursesController(APIClient client)//dependency injection. the controller class depends on the client
        {
            _client = client;
        }

        public IActionResult Index()

        {


            return View();
        }
        public IActionResult GetCourses(string radius, string ZipCode)
        {
            var courses = _client.GolfCourses(ZipCode, radius);//calling the method in APIClient that returns the courses

            return View(courses);
        }
        public IActionResult GetCourseDetails(string zip, string name)
        {
            var course = _client.GolfCourseDetails(zip, name);
            return View(course);
        }
        public IActionResult GetWeather(string zip)
        {
            var weather = _client.GetTheWeather(zip);
            return View(weather);
        }

    }

}
