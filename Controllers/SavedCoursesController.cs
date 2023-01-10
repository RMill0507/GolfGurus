using GolfGurus.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfGurus.Controllers
{
    public class SavedCoursesController : Controller
    {
        // private field to hold an instance of IGolfCourseRepository which will be used to interact with the database
        private readonly IGolfCourseRepository _repo;

        // constructor that takes an IGolfCourseRepository as a parameter and assigns it to the _repo field
        public SavedCoursesController(IGolfCourseRepository repo)
        {
            this._repo = repo;
        }
        // method that returns an IActionResult
        public IActionResult Index()//Default index
        {
            // use the _repo object to call the GetAllCourses method
            var courses = _repo.GetAllCourses();
            // return the result as a view
            return View(courses);
        }
        // method that takes an object of 'GolfCourses' class and returns an IActionResult
        public IActionResult InsertCourseToDataBase(GolfCourses courses)
        {
            // use the _repo object to call the InsertCourseToDataBase method
            _repo.InsertCourseToDataBase(courses);
            // redirect to the Index action 
            return RedirectToAction("Index");
        }
        // method that takes an object of 'GolfCourses' class and returns an IActionResult
        public IActionResult DeleteCourse(GolfCourses course)
        {
            // use the _repo object to call the DeleteCourse method
            _repo.DeleteCourse(course);
            // redirect to the Index action
            return RedirectToAction("Index");
        }
    }
}
