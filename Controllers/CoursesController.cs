using GolfGurus.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfGurus.Controllers
{
    public class CoursesController : Controller
    {
        // private field to hold an instance of APIClient which will be used to interact with external APIs
        private readonly APIClient _client;
        
        // constructor that takes an APIClient as a parameter and assigns it to the _client field
        public CoursesController(APIClient client)
        {
            _client = client;
        }

        // method that returns an IActionResult
        public IActionResult Index()

        {
            // return the default view
            return View();
        }
        // method that takes two string arguments and returns an IActionResult
        public IActionResult GetCourses(string radius, string ZipCode)
        {
            // use the _client object to call the GolfCourses method, passing the two arguments
            var courses = _client.GolfCourses(ZipCode, radius);
            // return the result as a view
            return View(courses);
        }
        // method that takes two string arguments and returns an IActionResult
        public IActionResult GetCourseDetails(string zip, string name)
        {
            // use the _client object to call the GolfCourseDetails method, passing the two arguments
            var course = _client.GolfCourseDetails(zip, name);
            // return the result as a view
            return View(course);
        }
        // method that takes one string argument and returns an IActionResult
        public IActionResult GetWeather(string zip)
        {
            // use the _client object to call the GetTheWeather method, passing the argument
            var weather = _client.GetTheWeather(zip);
            // return the result as a view
            return View(weather);
        }

    }

}
