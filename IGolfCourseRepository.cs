using GolfGurus.Models;

namespace GolfGurus
{
    public interface IGolfCourseRepository
    {
        // method that inserts an object of "GolfCourses" class into the database
        public void InsertCourseToDataBase(GolfCourses courseToInsert);
        // method that retrieves all "GolfCourses" objects from the database
        public IEnumerable<GolfCourses> GetAllCourses();

        // method that takes an object of "GolfCourses" and deletes it from the database
        public void DeleteCourse(GolfCourses course);
    }

}
