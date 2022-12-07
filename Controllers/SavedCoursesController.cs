using GolfGurus.Models;
using Microsoft.AspNetCore.Mvc;

namespace GolfGurus.Controllers
{
    public class SavedCoursesController : Controller
    {
        private readonly IGolfCourseRepository _repo;
        
        public SavedCoursesController(IGolfCourseRepository repo)
        {
            this._repo = repo;
        }
        public IActionResult Index()//Default index
        {
            var courses = _repo.GetAllCourses();
            return View(courses);
        }
        public IActionResult InsertCourseToDataBase(GolfCourses courses)
        {
           _repo.InsertCourseToDataBase(courses);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCourse(GolfCourses course)
        {
            _repo.DeleteCourse(course);
            return RedirectToAction("Index");
        }
    }
}
